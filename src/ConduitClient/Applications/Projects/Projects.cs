namespace Stwalkerster.ConduitClient.Applications.Projects
{
    public class Projects : ApplicationEditorApi<Project, int>
    {
        public Projects(ConduitClient client)
            : base(client)
        {
        }

        protected override Project NewFromSearch(dynamic data)
        {
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
                dateModified: (int)data.fields.dateModified);
        }

        protected override string GetApplicationName()
        {
            return "project";
        }
    }
}