// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransactionalObject.cs" company="Simon Walker">
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The datatype of the identifier</typeparam>
    public abstract class TransactionalObject<T>
    {
        private readonly DateTime dateCreated;

        private readonly DateTime dateModified;

        private readonly Dictionary<string, Transaction> pendingTransactions = new Dictionary<string, Transaction>();

        protected TransactionalObject()
        {
        }

        protected TransactionalObject(int dateCreated, int dateModified)
        {
            this.dateCreated = new DateTime().AddSeconds(dateCreated);
            this.dateModified = new DateTime().AddSeconds(dateModified);
        }

        public DateTime DateCreated
        {
            get
            {
                return this.dateCreated;
            }
        }

        public DateTime DateModified
        {
            get
            {
                return this.dateModified;
            }
        }

        public T Identifier { get; internal set; }

        public string ObjectPHID { get; internal set; }

        public string Uri { get; internal set; }

        protected internal Dictionary<string, Transaction> PendingTransactions
        {
            get
            {
                return this.pendingTransactions;
            }
        }

        public void AddTransaction(string key, Transaction transaction)
        {
            this.pendingTransactions.Add(key, transaction);
        }

        internal List<Transaction> GetTransactions()
        {
            return new List<Transaction>(this.pendingTransactions.Values.Where(x => !x.Invalided));
        }

        internal void InvalidateTransactions()
        {
            foreach (var trans in this.pendingTransactions.Values)
            {
                trans.Invalided = true;
            }
        }

        /// <summary>
        /// Gets the current value of a property
        /// </summary>
        /// <para>
        /// This method should be combined with <see cref="SetValue{TV}"/> for proper behaviour.
        /// All property access for properties which have been set with SetValue(...) should be accessed
        /// via this method, or the properties will appear unedited.
        /// </para>
        /// <para>
        /// This method will:
        ///     a) Check for a pending transaction, and return the value in the transaction
        ///     b) If no transactions found, return the original value passed in.
        /// </para>
        /// <example>
        /// private string foo;
        /// public string Foo
        /// {
        ///     get
        ///     {
        ///         return this.GetValue("foo", this.foo);
        ///     }
        ///     set
        ///     {
        ///         this.SetValue(value, "foo", this.foo);
        ///     }
        /// }
        /// </example>
        /// <param name="type"></param>
        /// <param name="originalValue"></param>
        /// <returns></returns>
        protected TV GetValue<TV>(string type, TV originalValue)
        {
            Transaction transaction;
            if (this.pendingTransactions.TryGetValue(type, out transaction))
            {
                return (TV)transaction.Value;
            }

            return originalValue;
        }

        /// <summary>
        /// Sets a transactional property
        /// </summary>
        /// <para>
        /// This method is intended to be called from property setters to add an ApplicationEditor 
        /// transaction to the list of transactions to be applied to the object on save. For correct
        /// behaviour of the object, this should be combined with the <see cref="GetValue{TV}"/> method.
        /// Note that transactional properties will never edit the original value of the property under
        /// user control.
        /// </para>
        /// <para>
        /// This method will:
        ///     a) check for a pending transaction, and update the value on the pending transaction
        ///     b) add a new pending transaction if a transaction cannot be found
        ///     c) remove a pending transaction if the value is being returned to the original value of the field
        /// </para>
        /// <param name="value">The value to set the property to</param>
        /// <param name="type">The transaction name</param>
        /// <param name="originalValue">The original value of the property</param>
        protected void SetValue<TV>(TV value, string type, TV originalValue)
        {
            Transaction transaction;
            if (this.pendingTransactions.TryGetValue(type, out transaction))
            {
                if (Equals(value, originalValue))
                {
                    this.pendingTransactions.Remove(type);
                    return;
                }

                transaction.Value = value;
            }
            else
            {
                if (!Equals(value, originalValue))
                {
                    transaction = new Transaction { Type = type, Value = value };
                    this.pendingTransactions.Add(transaction.Type, transaction);
                }
            }
        }
    }
}