using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stwalkerster.SharphConduit.Applications.Diffusion
{
    using System.Diagnostics;

    [DebuggerDisplay("r{CallsignOrId} {name}")]
    public class Repository : TransactionalObject<int>
    {
        private readonly string name;

        private readonly string vcs;

        private readonly string callsign;

        private readonly string shortName;

        private readonly string status;

        private readonly bool isImporting;

        private readonly string spacePHID;

        private readonly int dateCreated;

        private readonly int dateModified;

        private readonly string view;

        private readonly string edit;

        private readonly string push;

        public Repository()
        {
        }

        public Repository(
            string phid,
            int identifier,
            string uri,
            string name,
            string vcs,
            string callsign,
            string shortName,
            string status,
            bool isImporting,
            string spacePHID,
            int dateCreated,
            int dateModified,
            string view,
            string edit,
            string push)
            : base(dateCreated, dateModified)
        {
            this.ObjectPHID = phid;
            this.Identifier = identifier;
            this.Uri = uri;

            this.name = name;
            this.vcs = vcs;
            this.callsign = callsign;
            this.shortName = shortName;
            this.status = status;
            this.isImporting = isImporting;
            this.spacePHID = spacePHID;
            this.view = view;
            this.edit = edit;
            this.push = push;
        }

        public string CallsignOrId
        {
            get
            {
                if (this.callsign == null)
                {
                    return this.Identifier.ToString();
                }
                return this.callsign;
            }
        }

        public string Name
        {
            get
            {
                return this.GetValue("name", this.name);
            }
            set
            {
                this.SetValue(value, "name", this.name);
            }
        }

        public string Vcs
        {
            get
            {
                return this.GetValue("vcs", this.vcs);
            }
            set
            {
                this.SetValue(value, "vcs", this.vcs);
            }
        }

        public string Callsign
        {
            get
            {
                return this.GetValue("callsign", this.callsign);
            }
            set
            {
                this.SetValue(value, "callsign", this.callsign);
            }
        }

        public string ShortName
        {
            get
            {
                return this.GetValue("shortName", this.shortName);
            }
            set
            {
                this.SetValue(value, "shortName", this.shortName);
            }
        }

        public string Status
        {
            get
            {
                return this.GetValue("status", this.status);
            }
            set
            {
                this.SetValue(value, "status", this.status);
            }
        }

        public bool IsImporting
        {
            get
            {
                return this.isImporting;
            }
        }

        public string SpacePHID
        {
            get
            {
                return this.GetValue("space", this.spacePHID);
            }
            set
            {
                this.SetValue(value, "space", this.spacePHID);
            }
        }

        public string ViewPolicy
        {
            get
            {
                return this.GetValue("view", this.view);
            }
            set
            {
                this.SetValue(value, "view", this.view);
            }
        }

        public string EditPolicy
        {
            get
            {
                return this.GetValue("edit", this.edit);
            }
            set
            {
                this.SetValue(value, "edit", this.edit);
            }
        }

        public string PushPolicy
        {
            get
            {
                return this.GetValue("policy.push", this.push);
            }
            set
            {
                this.SetValue(value, "policy.push", this.push);
            }
        }
    }
}
