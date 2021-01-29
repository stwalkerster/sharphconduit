// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectColor.cs" company="Simon Walker">
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

    public class ProjectColor : ConduitLookupBase<ProjectColor>
    {
        public static ProjectColor Blue = new ProjectColor("blue");

        public static ProjectColor Checkered = new ProjectColor("checkered");

        public static ProjectColor Green = new ProjectColor("green");

        public static ProjectColor Grey = new ProjectColor("grey");

        public static ProjectColor Indigo = new ProjectColor("indigo");

        public static ProjectColor Orange = new ProjectColor("orange");

        public static ProjectColor Pink = new ProjectColor("pink");

        public static ProjectColor Red = new ProjectColor("red");

        public static ProjectColor Violet = new ProjectColor("violet");

        public static ProjectColor Yellow = new ProjectColor("yellow");

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