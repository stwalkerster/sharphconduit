// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Simon Walker">
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
namespace Stwalkerster.ConduitTestApplication
{
    using System.Collections.Generic;

    using Stwalkerster.SharphConduit;
    using Stwalkerster.SharphConduit.Applications;
    using Stwalkerster.SharphConduit.Applications.Maniphest;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            // --------------------------------------------------------------------//
            // Don't change this from scimonshouse.net or you'll leak credentials! //
            // This is an internal-only development install
            // --------------------------------------------------------------------//
            string phabUrl = "https://phabricator-dev.scimonshouse.net/";
            string token = "api-afsnrvwx2jkls47ti7ododlqnxwz";

            var client = new ConduitClient(phabUrl, token);
            
            var maniphest = new Maniphest(client);

            var constraint = ManiphestSearchConstraintFactory.Statuses(new List<string> { "open" });
            var searchConstraints = new List<ApplicationEditorSearchConstraint> { constraint };
            IEnumerable<ManiphestTask> response = maniphest.Search(constraints: searchConstraints);

            var task = new ManiphestTask();

            task.Title = "Example task";
            task.Description = "Example description";
            
            maniphest.Edit(task);
        }
    }
}