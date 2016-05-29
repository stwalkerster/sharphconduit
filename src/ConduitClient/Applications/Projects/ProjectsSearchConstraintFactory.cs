namespace Stwalkerster.ConduitClient.Applications.Projects
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ProjectsSearchConstraintFactory
    {
        public static ApplicationEditorSearchConstraint Name(string projectName)
        {
            return new ApplicationEditorSearchConstraint("name", projectName);
        }

        public static ApplicationEditorSearchConstraint Members(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("memberPHIDs", users);
        }

        public static ApplicationEditorSearchConstraint Watchers(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("watcherPHIDs", users);
        }

        public static ApplicationEditorSearchConstraint Icons(List<ProjectIcon> icons)
        {
            return new ApplicationEditorSearchConstraint("icons", icons.Select(x => x.ApiName));
        }

        public static ApplicationEditorSearchConstraint Colors(List<ProjectColor> colors)
        {
            return new ApplicationEditorSearchConstraint("colors", colors.Select(x => x.ApiName));
        }
    }
}