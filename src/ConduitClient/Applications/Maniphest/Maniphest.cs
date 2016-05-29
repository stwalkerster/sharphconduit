namespace Stwalkerster.ConduitClient.Applications.Maniphest
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public class Maniphest : ApplicationEditorApi<ManiphestTask, int>
    {
        public Maniphest(ConduitClient client)
            : base(client)
        {
        }

        protected override ManiphestTask NewFromSearch(dynamic data)
        {
            throw new System.NotImplementedException();
        }

        protected override string GetApplicationName()
        {
            return "maniphest";
        }

        public ManiphestTask Info(int taskId)
        {
            dynamic result = this.ConduitClient.CallMethod(
                "maniphest.info",
                new Dictionary<string, dynamic> { { "task_id", taskId } });

            var rawtask = result.result;

            string phid = rawtask.phid;
            int identifier = (int)rawtask.id;
            string uri = rawtask.uri;
            string title = rawtask.title;
            string description = rawtask.description;
            string status = rawtask.status;
            string priority = rawtask.priority;
            string owner = rawtask.ownerPHID;
            string author = rawtask.authorPHID;
            string space = rawtask.space;
            int? points = rawtask.points;
            int dateCreated = rawtask.dateCreated;
            int dateModified = rawtask.dateModified;

            var rawProjectPHIDs = (JArray)rawtask.projectPHIDs;
            List<string> projectPHIDs = rawProjectPHIDs != null ? new List<string>(rawProjectPHIDs.Values<string>()) : new List<string>();

            var rawSubscriberPHIDs = (JArray)rawtask.subscriberPHIDs;
            List<string> subscriberPHIDs = rawSubscriberPHIDs != null ? new List<string>(rawSubscriberPHIDs.Values<string>()) : new List<string>();

            var maniphestTask = new ManiphestTask(
                phid: phid,
                identifier: identifier,
                uri: uri,
                title: title,
                description: description,
                status: status,
                parent: null,
                priority: priority,
                owner: owner,
                author: author,
                space: space,
                points: points,
                dateCreated: dateCreated,
                dateModified: dateModified,
                projectPHIDs: projectPHIDs,
                subscriberPHIDs: subscriberPHIDs
                );

            return maniphestTask;

            //// {
            ////   "ccPHIDs": [
            ////     "PHID-USER-ure4heq4jqxak7dlmxqg"
            ////   ],
            ////   "projectPHIDs": [],
            ////   "auxiliary": [],
            ////   "objectName": "T2",
            ////   "dependsOnTaskPHIDs": []
            //// }
        }
    }
}