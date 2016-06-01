namespace Stwalkerster.ConduitClient.Applications.Maniphest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Newtonsoft.Json.Linq;

    public class Maniphest : ApplicationEditorApi<ManiphestTask, int>
    {
        public Maniphest(ConduitClient client)
            : base(client)
        {
        }

        protected override ManiphestTask NewFromSearch(dynamic data)
        {
            var subscribers= new List<string>();
            var projects = new List<string>();

            if (data.attachments != null)
            {
                if (data.attachments.projects != null)
                {
                    var jArray = (JArray)data.attachments.projects.projectPHIDs;
                    projects = new List<string>(jArray.Values<string>());
                }

                if (data.attachments.subscribers != null)
                {
                    var jArray = (JArray)data.attachments.subscribers.subscriberPHIDs;
                    subscribers = new List<string>(jArray.Values<string>());
                }
            }

            var customFields =
                ((JObject)data.fields).AsJEnumerable()
                    .ToList()
                    .Where(x => ((JProperty)x).Name.StartsWith("custom."))
                    .ToDictionary(x => ((JProperty)x).Name, y => (dynamic)((JProperty)y).Value);

            var task = new ManiphestTask(
                phid: (string)data.phid,
                identifier: (int)data.id,
                uri: null,
                title: (string)data.fields.name,
                description: null,
                status: (string)data.fields.status.value,
                parent: null,
                priority: (string)data.fields.priority.value,
                owner: (string)data.fields.ownerPHID,
                author: (string)data.fields.authorPHID,
                space: (string)data.fields.spacePHID,
                points: (int?)data.fields.points,
                viewPolicy: (string)data.fields.policy.view,
                editPolicy: (string)data.fields.policy.edit,
                dateCreated: (int)data.fields.dateCreated,
                dateModified: (int)data.fields.dateModified,
                projectPHIDs: projects, 
                subscriberPHIDs: subscribers,
                customFields: customFields);

            return task;
        }

        protected override string GetApplicationName()
        {
            return "maniphest";
        }

        public ManiphestTask Info(int taskId)
        {
            return this.Search(null, new[] { new ApplicationEditorSearchConstraint("ids", new[] { taskId }) }).FirstOrDefault();
        }
    }
}