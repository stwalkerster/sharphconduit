namespace Stwalkerster.ConduitClient
{
    using System.Collections.Generic;
    using System.Linq;

    using Stwalkerster.ConduitClient.Applications.Projects;

    public abstract class ConduitLookupBase<T>
        where T : ConduitLookupBase<T>
    {
        protected static IDictionary<string, T> LookupMap;

        private readonly string apiName;

        protected ConduitLookupBase(string apiName)
        {
            this.apiName = apiName;
        }

        public string ApiName
        {
            get
            {
                return this.apiName;
            }
        }

        public static T FromApiName(string apiName)
        {
            T col;
            if (apiName != null && LookupMap.TryGetValue(apiName, out col))
            {
                return col;
            }

            return null;
        }
        
        protected static void SetupLookupMap(IEnumerable<T> items)
        {
            LookupMap = items.ToDictionary(x => x.ApiName);
        }
    }
}