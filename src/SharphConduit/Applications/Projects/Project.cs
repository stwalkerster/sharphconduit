// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Project.cs" company="Simon Walker">
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
    using System.Linq;

    /// <summary>
    ///     TODO: members
    ///     TODO: slugs
    ///     TODO: view + edit + join policies
    ///     TODO: parent + milestone on creation
    /// </summary>
    public class Project : TransactionalObject<int>
    {
        private readonly string color;

        private readonly string description;

        private readonly string icon;

        private readonly List<string> memberPHIDs;

        private readonly string name;

        private readonly List<string> watcherPHIDs;

        public Project()
        {
        }

        internal Project(
            string phid,
            int identifier,
            string uri,
            string color,
            string icon,
            string name,
            string description,
            string viewPolicy,
            string editPolicy,
            string joinPolicy,
            int dateCreated,
            int dateModified,
            IEnumerable<string> memberPHIDs,
            IEnumerable<string> watcherPHIDs)
            : base(dateCreated, dateModified)
        {
            this.color = color;
            this.icon = icon;
            this.name = name;
            this.description = description;
            this.memberPHIDs = memberPHIDs.ToList();
            this.watcherPHIDs = watcherPHIDs.ToList();

            this.ObjectPHID = phid;
            this.Identifier = identifier;
            this.Uri = uri;
        }

        public ProjectColor Color
        {
            get
            {
                return ProjectColor.FromApiName(this.GetValue("color", this.color));
            }
            set
            {
                this.SetValue(value.ApiName, "color", this.color);
            }
        }

        public string Description
        {
            get
            {
                return this.GetValue("description", this.description);
            }
            set
            {
                this.SetValue(value, "description", this.description);
            }
        }

        public ProjectIcon Icon
        {
            get
            {
                return ProjectIcon.FromApiName(this.GetValue("icon", this.icon));
            }
            set
            {
                this.SetValue(value.ApiName, "icon", this.icon);
            }
        }

        public List<string> MemberPHIDs
        {
            get
            {
                var enumerable = new List<string>(this.memberPHIDs);
                // TODO: make this reflect the pending transactions
                return enumerable;
            }
        }

        public string Name
        {
            get
            {
                return this.GetValue("name", this.name);
            }
            set
            {
                this.SetValue(value, "name", this.name);
            }
        }

        public List<string> WatcherPHIDs
        {
            get
            {
                var enumerable = new List<string>(this.watcherPHIDs);
                // TODO: make this reflect the pending transactions
                return enumerable;
            }
        }
    }
}