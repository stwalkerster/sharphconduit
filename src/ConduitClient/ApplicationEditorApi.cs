namespace Stwalkerster.ConduitClient
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ApplicationEditorApi<T, TId> where T : TransactionalObject<TId>
    {
        private readonly ConduitClient client;

        protected abstract T NewFromSearch(dynamic data);

        public IEnumerable<T> Search(string baseQuery = null, IEnumerable<ApplicationEditorSearchConstraint> constraints = null, IEnumerable<string> attachments = null)
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
            
            while(true)
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

                if (query.ContainsKey("after")) { query.Remove("after");}

                query.Add("after", result.cursor.after);
            }
        }

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

        protected abstract string GetApplicationName();

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
    }
}