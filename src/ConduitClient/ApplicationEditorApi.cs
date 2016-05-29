namespace Stwalkerster.ConduitClient
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public abstract class ApplicationEditorApi<T> where T : TransactionalObject
    {
        private readonly ConduitClient client;

        //public List<T> Search(object something);

        protected ApplicationEditorApi(ConduitClient client)
        {
            this.client = client;
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

            if (result.error_code != null)
            {
                throw new ConduitException(result.error_code, result.error_info);
            }

            transactionalObject.ObjectPHID = result.result.@object.phid;
            transactionalObject.Identifier = result.result.@object.id;
        }
    }
}