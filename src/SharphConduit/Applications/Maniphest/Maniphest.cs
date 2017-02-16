// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Maniphest.cs" company="Simon Walker">
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

namespace Stwalkerster.SharphConduit.Applications.Maniphest
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json.Linq;

    public class Maniphest : ApplicationEditorApi<ManiphestTask, int>
    {
        public Maniphest(ConduitClient client)
            : base(client)
        {
        }

        public ManiphestTask Info(int taskId)
        {
            return
                this.Search(null, new[] { new ApplicationEditorSearchConstraint("ids", new[] { taskId }) })
                    .FirstOrDefault();
        }

        protected override string GetApplicationName()
        {
            return "maniphest";
        }

        protected override ManiphestTask NewFromSearch(dynamic data)
        {
            var subscribers = new List<string>();
            var projects = new List<string>();
            var workboardColumns = new Dictionary<string, List<string>>();

            if (data["attachments"] != null)
            {
                if (data["attachments"]["projects"] != null)
                {
                    var projectPHIDs = (JArray)data["attachments"]["projects"]["projectPHIDs"];
                    projects = new List<string>(projectPHIDs.Values<string>());
                }

                if (data["attachments"]["subscribers"] != null)
                {
                    var subscriberPHIDs = (JArray)data["attachments"]["subscribers"]["subscriberPHIDs"];
                    subscribers = new List<string>(subscriberPHIDs.Values<string>());
                }

                if (data["attachments"]["columns"] != null)
                {
                    var boards =(JObject) data["attachments"]["columns"]["boards"];

                    var cols = new Dictionary<string, List<string>>();

                    foreach (var board in boards)
                    {
                        cols.Add(board.Key, board.Value["columns"].Select(col => (string)col["phid"]).ToList());
                    }

                    workboardColumns = cols;
                }
            }

            var customFields =
                ((JObject)data["fields"]).AsJEnumerable()
                    .ToList()
                    .Where(x => ((JProperty)x).Name.StartsWith("custom."))
                    .ToDictionary(x => ((JProperty)x).Name, y => (dynamic)((JProperty)y).Value);

            var task = new ManiphestTask(
                phid: (string)data["phid"],
                identifier: (int)data["id"],
                uri: null,
                title: (string)data["fields"]["name"],
                description: null,
                status: (string)data["fields"]["status"]["value"],
                parent: null,
                priority: (string)data["fields"]["priority"]["value"],
                owner: (string)data["fields"]["ownerPHID"],
                author: (string)data["fields"]["authorPHID"],
                space: (string)data["fields"]["spacePHID"],
                points: (int?)data["fields"]["points"],
                viewPolicy: (string)data["fields"]["policy"]["view"],
                editPolicy: (string)data["fields"]["policy"]["edit"],
                dateCreated: (int)data["fields"]["dateCreated"],
                dateModified: (int)data["fields"]["dateModified"],
                projectPHIDs: projects,
                subscriberPHIDs: subscribers,
                customFields: customFields,
                workboardColumns: workboardColumns);

            return task;
        }
    }
}