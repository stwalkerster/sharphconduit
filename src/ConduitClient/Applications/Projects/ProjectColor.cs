namespace Stwalkerster.ConduitClient.Applications.Projects
{
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectColor : ConduitLookupBase<ProjectColor>
    {
        public static ProjectColor Red = new ProjectColor("red");

        public static ProjectColor Orange = new ProjectColor("orange");

        public static ProjectColor Yellow = new ProjectColor("yellow");

        public static ProjectColor Green = new ProjectColor("green");

        public static ProjectColor Blue = new ProjectColor("blue");

        public static ProjectColor Indigo = new ProjectColor("indigo");

        public static ProjectColor Violet = new ProjectColor("violet");

        public static ProjectColor Pink = new ProjectColor("pink");

        public static ProjectColor Grey = new ProjectColor("grey");

        public static ProjectColor Checkered = new ProjectColor("checkered");

        static ProjectColor()
        {
            SetupLookupMap(new[] { Red, Orange, Yellow, Green, Blue, Indigo, Violet, Pink, Grey, Checkered });
        }

        private ProjectColor(string apiName)
            : base(apiName)
        {
        }
    }
}