namespace Stwalkerster.ConduitClient.Applications
{
    public class Maniphest : ApplicationEditorApi<ManiphestTask>
    {
        public Maniphest(ConduitClient client)
            : base(client)
        {
        }

        protected override string GetApplicationName()
        {
            return "maniphest";
        }
    }
}