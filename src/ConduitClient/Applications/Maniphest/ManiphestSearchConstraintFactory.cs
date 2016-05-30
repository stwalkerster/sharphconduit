namespace Stwalkerster.ConduitClient.Applications.Maniphest
{
    using System.Collections.Generic;

    public static class ManiphestSearchConstraintFactory
    {
        public static ApplicationEditorSearchConstraint AssignedTo(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("assigned", users);
        }

        public static ApplicationEditorSearchConstraint Authors(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("authors", users);
        }

        public static ApplicationEditorSearchConstraint Statuses(List<string> statuses)
        {
            return new ApplicationEditorSearchConstraint("statuses", statuses);
        }

        public static ApplicationEditorSearchConstraint Priorities(List<int> priorities)
        {
            return new ApplicationEditorSearchConstraint("priorities", priorities);
        }
    }
}