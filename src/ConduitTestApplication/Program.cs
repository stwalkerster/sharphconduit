namespace Stwalkerster.ConduitTestApplication
{
    using Stwalkerster.ConduitClient;
    using Stwalkerster.ConduitClient.Applications;

    class Program
    {
        static void Main(string[] args)
        {            
            string phabUrl = "https://phabricator.stwalkerster.co.uk/";
            string token = "api-iwacuvkmh5v753pvjiewdayncutn";

            var client = new ConduitClient(phabUrl, token);

            var maniphest = new Maniphest(client);
            var task = new ManiphestTask("Test task");
            task.Description = "woo test!";
           
            maniphest.Edit(task);
        }
    }
}
