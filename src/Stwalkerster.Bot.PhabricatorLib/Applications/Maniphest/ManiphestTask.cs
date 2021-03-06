﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ManiphestTask.cs" company="Simon Walker">
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
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Stwalkerster.Bot.PhabricatorLib;
    using Stwalkerster.Bot.PhabricatorLib.Utility;

    /// <summary>
    ///     Represents a task in Maniphest
    /// </summary>
    [DebuggerDisplay("T{Identifier} {title}")]
    public class ManiphestTask : TransactionalObject<int>
    {
        private readonly string author;

        private readonly IDictionary<string, dynamic> customFields;

        private readonly IDictionary<string, List<string>> workboardColumns;

        private readonly string description;

        private readonly string editPolicy;

        private readonly string owner;

        private readonly string parent;

        private readonly double? points;

        private readonly string priority;

        private readonly List<string> projectPHIDs;

        private readonly string space;

        private readonly string status;

        private readonly List<string> subscriberPHIDs;

        private readonly string title;

        private readonly string viewPolicy;

        public ManiphestTask()
        {
            this.projectPHIDs = new List<string>();
            this.subscriberPHIDs = new List<string>();

            this.customFields = new Dictionary<string, dynamic>();
            this.workboardColumns = new Dictionary<string, List<string>>();
        }

        internal ManiphestTask(
            string phid,
            int identifier,
            string uri,
            string title,
            string description,
            string status,
            string parent,
            string priority,
            string owner,
            string author,
            string space,
            double? points,
            string viewPolicy,
            string editPolicy,
            int dateCreated,
            int dateModified,
            IEnumerable<string> projectPHIDs,
            IEnumerable<string> subscriberPHIDs,
            IDictionary<string, dynamic> customFields,
            IDictionary<string, List<string>> workboardColumns)
            : base(dateCreated, dateModified)
        {
            this.ObjectPHID = phid;
            this.Identifier = identifier;
            this.Uri = uri;

            this.title = title;
            this.description = description;
            this.status = status;
            this.parent = parent;
            this.priority = priority;
            this.owner = owner;
            this.author = author;
            this.space = space;
            this.points = points;
            this.viewPolicy = viewPolicy;
            this.editPolicy = editPolicy;

            this.customFields = customFields;
            this.workboardColumns = workboardColumns;

            this.projectPHIDs = projectPHIDs.ToList();
            this.subscriberPHIDs = subscriberPHIDs.ToList();
        }

        public string Author
        {
            get { return this.author; }
        }

        public IDictionary<string, dynamic> CustomFields
        {
            get { return new Dictionary<string, dynamic>(this.customFields); }
        }

        public string Description
        {
            get { return this.GetValue("description", this.description); }

            set { this.SetValue(value, "description", this.description); }
        }

        public string EditPolicy
        {
            get { return this.GetValue("edit", this.editPolicy); }

            set { this.SetValue(value, "edit", this.editPolicy); }
        }

        public string Owner
        {
            get { return this.GetValue("owner", this.owner); }

            set { this.SetValue(value, "owner", this.owner); }
        }

        public string Parent
        {
            get { return this.GetValue("parent", this.parent); }

            set { this.SetValue(value, "parent", this.parent); }
        }

        public double? Points
        {
            get { return this.GetValue("points", this.points); }

            set { this.SetValue(value, "points", this.points); }
        }

        /// <summary>
        ///     HEY! You should SET this to a string value.
        ///     This means we read this as "High", but have to pass in "80" to set it to this. This seems silly.
        /// </summary>
        public string Priority
        {
            get { return this.GetValue("priority", this.priority); }

            set { this.SetValue(value, "priority", this.priority); }
        }

        public IEnumerable<string> Projects
        {
            get
            {
                var enumerable = new List<string>(this.projectPHIDs);
                // TODO: make this reflect the pending transactions - T575
                return enumerable;
            }
        }

        public string Space
        {
            get { return this.GetValue("space", this.space); }

            set { this.SetValue(value, "space", this.space); }
        }

        public string Status
        {
            get { return this.GetValue("status", this.status); }

            set { this.SetValue(value, "status", this.status); }
        }

        public IEnumerable<string> Subscribers
        {
            get
            {
                var enumerable = new List<string>(this.subscriberPHIDs);
                // TODO: make this reflect the pending transactions - T575
                return enumerable;
            }
        }

        public string Title
        {
            get { return this.GetValue("title", this.title); }

            set { this.SetValue(value, "title", this.title); }
        }

        public string ViewPolicy
        {
            get { return this.GetValue("view", this.viewPolicy); }

            set { this.SetValue(value, "view", this.viewPolicy); }
        }

        public ReadOnlyDictionary<string, ReadOnlyCollection<string>> WorkboardColumns
        {
            get
            {
                return
                    new ReadOnlyDictionary<string, ReadOnlyCollection<string>>(
                        this.workboardColumns.ToDictionary(
                            col => col.Key,
                            col => new ReadOnlyCollection<string>(col.Value)));
            }
        }

        public void AddComment(string text)
        {
            this.PendingTransactions.Add(
                RandomProvider.Next().ToString(),
                new Transaction { Type = "comment", Value = text });
        }

        public void AddProjects(string project)
        {
            this.AddProjects(new[] { project });
        }

        public void AddProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add("projects.add", new Transaction { Type = "projects.add", Value = projects });
        }

        public void AddSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.add",
                new Transaction { Type = "subscribers.add", Value = subscribers });
        }

        public void RemoveProjects(string project)
        {
            this.RemoveProjects(new[] { project });
        }

        public void RemoveProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add(
                "projects.remove",
                new Transaction { Type = "projects.remove", Value = projects });
        }

        public void RemoveSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.remove",
                new Transaction { Type = "subscribers.remove", Value = subscribers });
        }

        public void SetProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add("projects.set", new Transaction { Type = "projects.set", Value = projects });
        }

        public void SetSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.set",
                new Transaction { Type = "subscribers.set", Value = subscribers });
        }
    }
}