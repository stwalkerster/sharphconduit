// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationEditorApi.cs" company="Simon Walker">
//   Copyright (c) 2016 Simon Walker
//   -
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above 
//   copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//   Software.
//   -
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//   IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stwalkerster.SharphConduit
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ApplicationEditorApi<T, TId>
        where T : TransactionalObject<TId>
    {
        private readonly ConduitClient client;

        protected ApplicationEditorApi(ConduitClient client)
        {
            this.client = client;
        }

        public ConduitClient ConduitClient
        {
            get
            {
                return this.client;
            }
        }

        public void Edit(T transactionalObject)
        {
            dynamic result = this.client.CallMethod(
                string.Format("{0}.edit", this.GetApplicationName()),
                new Dictionary<string, dynamic>
                    {
                        { "objectIdentifier", transactionalObject.ObjectPHID },
                        { "transactions", transactionalObject.GetTransactions() }
                    });

            transactionalObject.ObjectPHID = result.result.@object.phid;
            transactionalObject.Identifier = result.result.@object.id;

            // invalidates the transactions. Note, we can't "apply" the transaction to the object
            // because we don't know the mapping to the internal fields. We also don't know which succeeded.
            // TODO: fix this.
            transactionalObject.InvalidateTransactions();
        }

        public IEnumerable<T> Search(
            string baseQuery = null,
            IEnumerable<ApplicationEditorSearchConstraint> constraints = null,
            IEnumerable<string> attachments = null)
        {
            var query = new Dictionary<string, dynamic>();

            if (baseQuery != null)
            {
                query.Add("queryKey", baseQuery);
            }

            if (constraints != null)
            {
                var constraintDictionary = new Dictionary<string, dynamic>();
                query.Add("constraints", constraintDictionary);
                foreach (var constraint in constraints)
                {
                    constraintDictionary.Add(constraint.Type, constraint.Value);
                }
            }

            if (attachments != null)
            {
                query.Add("attachments", attachments.ToDictionary(x => x, x => true));
            }

            // TODO: ordering

            while (true)
            {
                dynamic response = this.client.CallMethod(string.Format("{0}.search", this.GetApplicationName()), query);
                dynamic result = response.result;

                foreach (var entry in result.data)
                {
                    yield return this.NewFromSearch(entry);
                }

                if (result.cursor.after == null)
                {
                    yield break;
                }

                if (query.ContainsKey("after"))
                {
                    query.Remove("after");
                }

                query.Add("after", result.cursor.after);
            }
        }

        protected abstract string GetApplicationName();

        protected abstract T NewFromSearch(dynamic data);
    }
}