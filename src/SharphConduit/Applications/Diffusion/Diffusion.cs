using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stwalkerster.SharphConduit.Applications.Diffusion
{
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The diffusion.
    /// </summary>
    public class Diffusion : ApplicationEditorApi<Repository, int>
    {
        public Diffusion(ConduitClient client)
            : base(client)
        {
        }

        protected override string GetApplicationName()
        {
            return "diffusion.repository";
        }

        protected override Repository NewFromSearch(dynamic data)
        {
            var repo = new Repository(
                (string)data.phid,
                (int)data.id,
                null,
                (string)data.fields.name,
                (string)data.fields.vcs,
                (string)data.fields.callsign,
                (string)data.fields.shortName,
                (string)data.fields.status,
                (bool)data.fields.isImporting,
                (string)data.fields.spacePHID,
                (int)data.fields.dateCreated,
                (int)data.fields.dateModified,
                (string)data.fields.policy.view,
                (string)data.fields.policy.edit,
                (string)((JObject)data.fields.policy)["diffusion.push"]);

            return repo;
        }
    }
}
