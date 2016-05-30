namespace Stwalkerster.ConduitClient.Applications.Projects
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public class Projects : ApplicationEditorApi<Project, int>
    {
        public Projects(ConduitClient client)
            : base(client)
        {
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
                watcherPHIDs: watchers);
        }

        protected override string GetApplicationName()
        {
            return "project";
        }
    }
}