namespace Stwalkerster.ConduitTestApplication
{
    using System.Collections.Generic;
    using System.Linq;

    using Stwalkerster.ConduitClient;
    using Stwalkerster.ConduitClient.Applications;
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
            
            var maniphest = new Maniphest(client);

            var maniphestTask = maniphest.Info(7);
            //var maniphestTask = new ManiphestTask();

            var phidLookup = new PHIDLookup(client);
            var phidForObject = phidLookup.GetPHIDForObject("S2", "#work_stuff");

            //maniphestTask.Title = "Test private task";
            //maniphestTask.Owner = "stwalkerster";
            //maniphestTask.AddProjects(new List<string> { "testing", "work_stuff"});
            //maniphestTask.Points = 4;
            //maniphestTask.Description = "New task for testing all sorts of crazy things with";
            //maniphestTask.Space = "PHID-SPCE-fqtvdv5vnsv6yhnzvyb4";

            //maniphestTask.ViewPolicy = "work_stuff";
            maniphestTask.EditPolicy = "#work_stuff";

            maniphest.Edit(maniphestTask);



        }
    }
}
