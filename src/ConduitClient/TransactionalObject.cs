namespace Stwalkerster.ConduitClient
{
    using System.Collections.Generic;

    public abstract class TransactionalObject
    {
        private readonly Dictionary<string, Transaction> pendingTransactions = new Dictionary<string, Transaction>();

        protected internal Dictionary<string, Transaction> PendingTransactions
        {
            get
            {
                return this.pendingTransactions;
            }
        }

        /// <summary>
        /// Gets the identifier of the object to be used when editing an existing object. This must be overridden in subclasses. 
        /// </summary>
        /// <returns></returns>
        public abstract object GetIdentifier();

        internal void ClearTransactions()
        {
            this.pendingTransactions.Clear();
        }

        internal List<Transaction> GetTransactions()
        {
            return new List<Transaction>(this.pendingTransactions.Values);
        }
        
        /// <summary>
        /// Gets the current value of a property
        /// </summary>
        /// <para>
        /// This method should be combined with <see cref="SetValue"/> for proper behaviour.
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
        protected string GetValue(string type, string originalValue)
        {
            Transaction transaction;
            if (this.pendingTransactions.TryGetValue(type, out transaction))
            {
                return (string)transaction.Value;
            }

            return originalValue;
        }

        /// <summary>
        /// Sets a transactional property
        /// </summary>
        /// <para>
        /// This method is intended to be called from property setters to add an ApplicationEditor 
        /// transaction to the list of transactions to be applied to the object on save. For correct
        /// behaviour of the object, this should be combined with the <see cref="GetValue"/> method.
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
        protected void SetValue(string value, string type, string originalValue)
        {
            Transaction transaction;
            if (this.pendingTransactions.TryGetValue(type, out transaction))
            {
                if (value == originalValue)
                {
                    this.pendingTransactions.Remove(type);
                    return;
                }

                transaction.Value = value;
            }
            else
            {
                if (value != originalValue)
                {
                    transaction = new Transaction { Type = type, Value = value };
                    this.pendingTransactions.Add(transaction.Type, transaction);
                }
            }
        }
    }
}