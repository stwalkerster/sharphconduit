// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectIcon.cs" company="Simon Walker">
//   Copyright (c) 2016 Simon Walker
//   -
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions: The above 
//   copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//   Software.
//   -
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//   IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Stwalkerster.Bot.PhabricatorLib.Applications.Projects
{
    using Stwalkerster.Bot.PhabricatorLib;

    public class ProjectIcon : ConduitLookupBase<ProjectIcon>
    {
        public static ProjectIcon Account = new ProjectIcon("account");

        public static ProjectIcon Bugs = new ProjectIcon("bugs");

        public static ProjectIcon Cleanup = new ProjectIcon("cleanup");

        public static ProjectIcon Communication = new ProjectIcon("communication");

        public static ProjectIcon Experimental = new ProjectIcon("experimental");

        public static ProjectIcon Folder = new ProjectIcon("folder");

        public static ProjectIcon Goal = new ProjectIcon("goal");

        public static ProjectIcon Group = new ProjectIcon("group");

        public static ProjectIcon Infrastructure = new ProjectIcon("infrastructure");
       
        public static ProjectIcon Milestone = new ProjectIcon("milestone");

        public static ProjectIcon Organization = new ProjectIcon("organization");

        public static ProjectIcon Policy = new ProjectIcon("policy");

        public static ProjectIcon Project = new ProjectIcon("project");

        public static ProjectIcon Release = new ProjectIcon("release");

        public static ProjectIcon Tag = new ProjectIcon("tag");

        public static ProjectIcon Timeline = new ProjectIcon("timeline");

        public static ProjectIcon Umbrella = new ProjectIcon("umbrella");

        static ProjectIcon()
        {
            SetupLookupMap(
                new[]
                    {
                        Project, Tag, Policy, Group, Folder, Timeline, Goal, Release, Bugs, Cleanup, Umbrella,
                        Communication, Organization, Infrastructure, Account, Experimental, Milestone
                    });
        }

        public ProjectIcon(string apiName)
            : base(apiName)
        {
        }
    }
}