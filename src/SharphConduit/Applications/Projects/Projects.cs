// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Projects.cs" company="Simon Walker">
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

namespace Stwalkerster.SharphConduit.Applications.Projects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    public class Projects : ApplicationEditorApi<Project, int>
    {
        public Projects(ConduitClient client)
            : base(client)
        {
            this.Columns = new ProjectWorkboardColumns(client);
        }

        protected override string GetApplicationName()
        {
            return "project";
        }

        protected override Project NewFromSearch(dynamic data)
        {
            var members = new List<string>();
            var watchers = new List<string>();

            if (data.attachments != null)
            {
                if (data.attachments.watchers != null)
                {
                    var jArray = (JArray)data.attachments.watchers.projectPHIDs;
                    watchers = new List<string>(jArray.Values<string>());
                }

                if (data.attachments.members != null)
                {
                    var jArray = (JArray)data.attachments.members.subscriberPHIDs;
                    members = new List<string>(jArray.Values<string>());
                }
            }

            var customFields =
                ((JObject)data["fields"]).AsJEnumerable()
                    .ToList()
                    .Where(x => ((JProperty)x).Name.StartsWith("custom."))
                    .ToDictionary(x => ((JProperty)x).Name, y => (dynamic)((JProperty)y).Value);

            return new Project(
                phid: (string)data.phid,
                identifier: (int)data.id,
                uri: null,
                color: (string)data.fields.color.key,
                icon: (string)data.fields.icon.key,
                name: (string)data.fields.name,
                description: (string)data.fields.description,
                viewPolicy: (string)data.fields.policy.view,
                editPolicy: (string)data.fields.policy.edit,
                joinPolicy: (string)data.fields.policy.join,
                dateCreated: (int)data.fields.dateCreated,
                dateModified: (int)data.fields.dateModified,
                memberPHIDs: members,
                watcherPHIDs: watchers,
                customFields: customFields);
        }

        public ProjectWorkboardColumns Columns { get; private set; }

        public class ProjectWorkboardColumns : ApplicationEditorApi<WorkboardColumn, int>
        {
            public ProjectWorkboardColumns(ConduitClient client)
                : base(client)
            {
            }

            protected override string GetApplicationName()
            {
                return "project.column";
            }

            protected override WorkboardColumn NewFromSearch(dynamic data)
            {
                var col = new WorkboardColumn(
                    phid: (string)data["phid"],
                    identifier: (int)data["id"],
                    uri: null,
                    name: (string)data["fields"]["name"],
                    project: (string)data["fields"]["project"]["phid"],
                    viewPolicy: (string)data["fields"]["policy"]["view"],
                    editPolicy: (string)data["fields"]["policy"]["edit"],
                    proxyPHID: (string)data["fields"]["proxyPHID"],
                    dateCreated: (int)data["fields"]["dateCreated"],
                    dateModified: (int)data["fields"]["dateModified"]);

                return col;
            }

            public override void Edit(WorkboardColumn transactionalObject)
            {
                throw new InvalidOperationException("This method is not available");
            }
        }
    }
}