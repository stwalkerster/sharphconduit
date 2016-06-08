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

            var maniphestTask = maniphest.Info(7);

            // var maniphestTask = new ManiphestTask();
            var phidLookup = new PHIDLookup(client);
            var phidForObject = phidLookup.GetPHIDForObject("S2", "#work_stuff");

            // maniphestTask.Title = "Test private task";
            // maniphestTask.Owner = "stwalkerster";
            // maniphestTask.AddProjects(new List<string> { "testing", "work_stuff"});
            // maniphestTask.Points = 4;
            // maniphestTask.Description = "New task for testing all sorts of crazy things with";
            // maniphestTask.Space = "PHID-SPCE-fqtvdv5vnsv6yhnzvyb4";

            // maniphestTask.ViewPolicy = "work_stuff";
            maniphestTask.EditPolicy = "#work_stuff";

            maniphest.Edit(maniphestTask);
        }
    }
}