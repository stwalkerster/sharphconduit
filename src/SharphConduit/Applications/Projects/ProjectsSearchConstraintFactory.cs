// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectsSearchConstraintFactory.cs" company="Simon Walker">
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
namespace Stwalkerster.ConduitClient.Applications.Projects
{
    using System.Collections.Generic;
    using System.Linq;

    using Stwalkerster.SharphConduit;
    using Stwalkerster.SharphConduit.Applications.Projects;

    /// <summary>
    /// The projects search constraint factory.
    /// </summary>
    public static class ProjectsSearchConstraintFactory
    {
        /// <summary>
        /// The colors.
        /// </summary>
        /// <param name="colors">
        /// The colors.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Colors(List<ProjectColor> colors)
        {
            return new ApplicationEditorSearchConstraint("colors", colors.Select(x => x.ApiName));
        }

        /// <summary>
        /// The icons.
        /// </summary>
        /// <param name="icons">
        /// The icons.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Icons(List<ProjectIcon> icons)
        {
            return new ApplicationEditorSearchConstraint("icons", icons.Select(x => x.ApiName));
        }

        /// <summary>
        /// The members.
        /// </summary>
        /// <param name="users">
        /// The users.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Members(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("memberPHIDs", users);
        }

        /// <summary>
        /// The name.
        /// </summary>
        /// <param name="projectName">
        /// The project name.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Name(string projectName)
        {
            return new ApplicationEditorSearchConstraint("name", projectName);
        }

        /// <summary>
        /// The watchers.
        /// </summary>
        /// <param name="users">
        /// The users.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Watchers(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("watcherPHIDs", users);
        }
    }
}