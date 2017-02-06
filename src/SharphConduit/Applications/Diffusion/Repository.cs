// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Simon Walker">
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

using System.Diagnostics;

namespace Stwalkerster.SharphConduit.Applications.Diffusion
{
    [DebuggerDisplay("r{CallsignOrId} {name}")]
    public class Repository : TransactionalObject<int>
    {
        private readonly string callsign;

        private readonly string editPolicy;

        private readonly bool isImporting;
        private readonly string name;

        private readonly string pushPolicy;

        private readonly string shortName;

        private readonly string spacePHID;

        private readonly string status;

        private readonly string vcs;

        private readonly string viewPolicy;

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
            string viewPolicy,
            string editPolicy,
            string pushPolicy)
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
            this.viewPolicy = viewPolicy;
            this.editPolicy = editPolicy;
            this.pushPolicy = pushPolicy;
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
            get { return this.GetValue("name", this.name); }
            set { this.SetValue(value, "name", this.name); }
        }

        public string Vcs
        {
            get { return this.GetValue("vcs", this.vcs); }
            set { this.SetValue(value, "vcs", this.vcs); }
        }

        public string Callsign
        {
            get { return this.GetValue("callsign", this.callsign); }
            set { this.SetValue(value, "callsign", this.callsign); }
        }

        public string ShortName
        {
            get { return this.GetValue("shortName", this.shortName); }
            set { this.SetValue(value, "shortName", this.shortName); }
        }

        public string Status
        {
            get { return this.GetValue("status", this.status); }
            set { this.SetValue(value, "status", this.status); }
        }

        public bool IsImporting
        {
            get { return this.isImporting; }
        }

        public string SpacePHID
        {
            get { return this.GetValue("space", this.spacePHID); }
            set { this.SetValue(value, "space", this.spacePHID); }
        }

        public string ViewPolicy
        {
            get { return this.GetValue("viewPolicy", this.viewPolicy); }
            set { this.SetValue(value, "viewPolicy", this.viewPolicy); }
        }

        public string EditPolicy
        {
            get { return this.GetValue("editPolicy", this.editPolicy); }
            set { this.SetValue(value, "editPolicy", this.editPolicy); }
        }

        public string PushPolicy
        {
            get { return this.GetValue("policy.push", this.pushPolicy); }
            set { this.SetValue(value, "policy.push", this.pushPolicy); }
        }
    }
}
