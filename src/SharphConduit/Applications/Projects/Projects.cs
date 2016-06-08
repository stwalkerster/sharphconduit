// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Projects.cs" company="Simon Walker">
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

namespace Stwalkerster.SharphConduit.Applications.Projects
{
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public class Projects : ApplicationEditorApi<Project, int>
    {
        public Projects(ConduitClient client)
            : base(client)
        {
        }

        protected override string GetApplicationName()
        {
            return "project";
        }

        protected override Project NewFromSearch(dynamic data)
        {
            var members = new List<string>();
            var watchers = new List<string>();

            if (data.attachments != null)
            {
                if (data.attachments.watchers != null)
                {
                    var jArray = (JArray)data.attachments.watchers.projectPHIDs;
                    watchers = new List<string>(jArray.Values<string>());
                }

                if (data.attachments.members != null)
                {
                    var jArray = (JArray)data.attachments.members.subscriberPHIDs;
                    members = new List<string>(jArray.Values<string>());
                }
            }

            return new Project(
                (string)data.phid,
                (int)data.id,
                null,
                (string)data.fields.color.key,
                (string)data.fields.icon.key,
                (string)data.fields.name,
                (string)data.fields.description,
                (string)data.fields.policy.view,
                (string)data.fields.policy.edit,
                (string)data.fields.policy.join,
                (int)data.fields.dateCreated,
                (int)data.fields.dateModified,
                members,
                watchers);
        }
    }
}