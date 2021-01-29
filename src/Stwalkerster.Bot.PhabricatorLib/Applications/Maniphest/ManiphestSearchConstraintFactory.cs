// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManiphestSearchConstraintFactory.cs" company="Simon Walker">
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
namespace Stwalkerster.Bot.PhabricatorLib.Applications.Maniphest
{
    using System.Collections.Generic;
    using Stwalkerster.Bot.PhabricatorLib;

    /// <summary>
    /// The maniphest search constraint factory.
    /// </summary>
    public static class ManiphestSearchConstraintFactory
    {
        /// <summary>
        /// The assigned to.
        /// </summary>
        /// <param name="users">
        /// The users.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint AssignedTo(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("assigned", users);
        }

        /// <summary>
        /// The authors.
        /// </summary>
        /// <param name="users">
        /// The users.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Authors(List<string> users)
        {
            return new ApplicationEditorSearchConstraint("authors", users);
        }

        /// <summary>
        /// The priorities.
        /// </summary>
        /// <param name="priorities">
        /// The priorities.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Priorities(List<int> priorities)
        {
            return new ApplicationEditorSearchConstraint("priorities", priorities);
        }

        /// <summary>
        /// The statuses.
        /// </summary>
        /// <param name="statuses">
        /// The statuses.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationEditorSearchConstraint"/>.
        /// </returns>
        public static ApplicationEditorSearchConstraint Statuses(List<string> statuses)
        {
            return new ApplicationEditorSearchConstraint("statuses", statuses);
        }
    }
}