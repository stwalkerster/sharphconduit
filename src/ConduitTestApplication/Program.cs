namespace Stwalkerster.ConduitTestApplication
{
    using Stwalkerster.ConduitClient;
    using Stwalkerster.ConduitClient.Applications;

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
            var task = new ManiphestTask { Title = "Test task", Description = "woo test!" };
            
            maniphest.Edit(task);

            task.Title = "EDITED test task!";
            task.AddComment("Woo! comment!");

            maniphest.Edit(task);
        }
    }
}
