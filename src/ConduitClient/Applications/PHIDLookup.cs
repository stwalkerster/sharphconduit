using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stwalkerster.ConduitClient.Applications
{
    using System.Diagnostics;

    using Newtonsoft.Json.Linq;

    public class PHIDLookup
    {
        private readonly ConduitClient client;

        public PHIDLookup(ConduitClient client)
        {
            this.client = client;
        }

        public Dictionary<string, string> GetPHIDForObject(params string[] identifiers)
        {
            var response = this.client.CallMethod(
                "phid.lookup",
                new Dictionary<string, dynamic> { { "names", identifiers } });

            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (JProperty x in response.result)
            {
                dynamic entry = x.Value;
                data.Add(x.Name, (string)entry.phid);
            }

            return data;
        }
    }
}
