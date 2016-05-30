namespace Stwalkerster.ConduitClient.Applications.Projects
{
    public class ProjectIcon : ConduitLookupBase<ProjectIcon>
    {
        public static ProjectIcon Project = new ProjectIcon("project");

        public static ProjectIcon Tag = new ProjectIcon("tag");

        public static ProjectIcon Policy = new ProjectIcon("policy");

        public static ProjectIcon Group = new ProjectIcon("group");

        public static ProjectIcon Folder = new ProjectIcon("folder");

        public static ProjectIcon Timeline = new ProjectIcon("timeline");

        public static ProjectIcon Goal = new ProjectIcon("goal");

        public static ProjectIcon Release = new ProjectIcon("release");

        public static ProjectIcon Bugs = new ProjectIcon("bugs");

        public static ProjectIcon Cleanup = new ProjectIcon("cleanup");

        public static ProjectIcon Umbrella = new ProjectIcon("umbrella");

        public static ProjectIcon Communication = new ProjectIcon("communication");

        public static ProjectIcon Organization = new ProjectIcon("organization");

        public static ProjectIcon Infrastructure = new ProjectIcon("infrastructure");

        public static ProjectIcon Account = new ProjectIcon("account");

        public static ProjectIcon Experimental = new ProjectIcon("experimental");

        static ProjectIcon()
        {
            SetupLookupMap(
                new[]
                    {
                        Project, Tag, Policy, Group, Folder, Timeline, Goal, Release, Bugs, Cleanup, Umbrella,
                        Communication, Organization, Infrastructure, Account, Experimental
                    });
        }

        public ProjectIcon(string apiName)
            : base(apiName)
        {
        }
    }
}