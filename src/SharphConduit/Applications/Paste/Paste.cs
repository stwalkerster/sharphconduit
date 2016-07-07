namespace Stwalkerster.SharphConduit.Applications.Paste
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    using Stwalkerster.SharphConduit;

    public class Paste : ApplicationEditorApi<PasteItem, int>
    {
        public Paste(ConduitClient client)
            : base(client)
        {
        }

        protected override PasteItem NewFromSearch(dynamic data)
        {
            var subscribers = new List<string>();
            var projects = new List<string>();

            string content = null;

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

                if (data.attachments.content != null)
                {
                    content = (string)data.attachments.content.content;
                }
            }

            return new PasteItem(
                phid: (string)data.phid,
                identifier: (int)data.id,
                title: (string)data.fields.title,
                text: content,
                status: (string)data.fields.status,
                language: (string)data.fields.language,
                space: (string)data.fields.spacePHID,
                viewPolicy: (string)data.fields.policy.view,
                editPolicy: (string)data.fields.policy.edit,
                    dateCreated: (int)data.fields.dateCreated,
                    dateModified: (int)data.fields.dateModified,
                projectPHIDs: projects,
                subscriberPHIDs: subscribers);
        }

        protected override string GetApplicationName()
        {
            return "paste";
        }
    }
}