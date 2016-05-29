namespace Stwalkerster.ConduitTestApplication
{
    using System.Collections.Generic;
    using System.Linq;

    using Stwalkerster.ConduitClient;
    using Stwalkerster.ConduitClient.Applications.Maniphest;
    using Stwalkerster.ConduitClient.Applications.Projects;

    class Program
    {
        static void Main(string[] args)
        {
            // --------------------------------------------------------------------//
            // Don't change this from scimonshouse.net or you'll leak credentials! //
            // --------------------------------------------------------------------//
            string phabUrl = "https://phabricator-dev.scimonshouse.net/";          //
            string token = "api-afsnrvwx2jkls47ti7ododlqnxwz";                     //
            // --------------------------------------------------------------------//

            var client = new ConduitClient(phabUrl, token);

            var project = new Projects(client);
            var maniphest = new Maniphest(client);

            var maniphestTask = maniphest.Info(4);

            //maniphestTask.Points = 4;
            //maniphestTask.Status = "open";
            //maniphestTask.Priority = "High";

            //maniphest.Edit(maniphestTask);

            //var result = client.CallMethod(
            //    "maniphest.edit",
            //    new Dictionary<string, dynamic>
            //        {
            //            { "objectIdentifier", 4 },
            //            {
            //                "transactions",
            //                new List<Transaction>
            //                    {
            //                        new Transaction
            //                            {
            //                                Type = "priority",
            //                                Value = "80"
            //                            }
            //                    }
            //            }
            //        });

            var list = maniphest.Search().ToList();
        }
    }
}
