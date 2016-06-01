namespace Stwalkerster.ConduitClient.Applications.Maniphest
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    ///     Represents a task in Maniphest
    /// </summary>
    /// <remarks>
    ///     TODO: parent
    ///     TODO: column
    ///     TODO: view + edit policies
    /// </remarks>
    [DebuggerDisplay("T{Identifier} {title}")]
    public class ManiphestTask : TransactionalObject<int>
    {
        private readonly string author;

        private readonly string description;

        private readonly string editPolicy;

        private readonly string owner;

        private readonly string parent;

        private readonly int? points;

        private readonly string priority;

        private readonly List<string> projectPHIDs;

        private readonly string space;

        private readonly string status;

        private readonly List<string> subscriberPHIDs;

        private readonly string title;

        private readonly string viewPolicy;

        private readonly IDictionary<string, dynamic> customFields;

        public ManiphestTask()
        {
            this.projectPHIDs = new List<string>();
            this.subscriberPHIDs = new List<string>();

            this.customFields = new Dictionary<string, dynamic>();
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
            int? points,
            string viewPolicy,
            string editPolicy,
            int dateCreated,
            int dateModified,
            IEnumerable<string> projectPHIDs,
            IEnumerable<string> subscriberPHIDs,
            IDictionary<string,dynamic> customFields)
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

            this.projectPHIDs = projectPHIDs.ToList();
            this.subscriberPHIDs = subscriberPHIDs.ToList();
        }

        public string Title
        {
            get
            {
                return this.GetValue("title", this.title);
            }

            set
            {
                this.SetValue(value, "title", this.title);
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

        /// <summary>
        ///     HEY! You should SET this to a string value.
        ///     This means we read this as "High", but have to pass in "80" to set it to this. This seems silly.
        /// </summary>
        public string Priority
        {
            get
            {
                return this.GetValue("priority", this.priority);
            }

            set
            {
                this.SetValue(value, "priority", this.priority);
            }
        }

        public string Status
        {
            get
            {
                return this.GetValue("status", this.status);
            }

            set
            {
                this.SetValue(value, "status", this.status);
            }
        }

        public string ViewPolicy
        {
            get
            {
                return this.GetValue("view", this.viewPolicy);
            }

            set
            {
                this.SetValue(value, "view", this.viewPolicy);
            }
        }

        public string EditPolicy
        {
            get
            {
                return this.GetValue("edit", this.editPolicy);
            }

            set
            {
                this.SetValue(value, "edit", this.editPolicy);
            }
        }

        public int? Points
        {
            get
            {
                return this.GetValue("points", this.points);
            }

            set
            {
                this.SetValue(value, "points", this.points);
            }
        }

        public string Space
        {
            get
            {
                return this.GetValue("space", this.space);
            }

            set
            {
                this.SetValue(value, "space", this.space);
            }
        }

        /// <summary>
        ///     TODO: PHID
        /// </summary>
        public string Parent
        {
            get
            {
                return this.GetValue("parent", this.parent);
            }

            set
            {
                this.SetValue(value, "parent", this.parent);
            }
        }

        /// <summary>
        ///     TODO: PHID/String/null
        /// </summary>
        public string Owner
        {
            get
            {
                return this.GetValue("owner", this.owner);
            }

            set
            {
                this.SetValue(value, "owner", this.owner);
            }
        }

        public string Author
        {
            get
            {
                return this.author;
            }
        }

        public IEnumerable<string> Projects
        {
            get
            {
                var enumerable = new List<string>(this.projectPHIDs);
                // TODO: make this reflect the pending transactions
                return enumerable;
            }
        }

        public IEnumerable<string> Subscribers
        {
            get
            {
                var enumerable = new List<string>(this.subscriberPHIDs);
                // TODO: make this reflect the pending transactions
                return enumerable;
            }
        }

        public void AddComment(string text)
        {
            string hash = BitConverter.ToString(SHA1.Create("SHA1").ComputeHash(Encoding.UTF8.GetBytes(text)));
            this.PendingTransactions.Add(hash, new Transaction { Type = "comment", Value = text });
        }

        public void AddProjects(string project)
        {
            this.AddProjects(new[] { project });
        }

        public void AddProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add("projects.add", new Transaction { Type = "projects.add", Value = projects });
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

        public void SetProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add("projects.set", new Transaction { Type = "projects.set", Value = projects });
        }

        public void AddSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.add",
                new Transaction { Type = "subscribers.add", Value = subscribers });
        }

        public void RemoveSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.remove",
                new Transaction { Type = "subscribers.remove", Value = subscribers });
        }

        public void SetSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.set",
                new Transaction { Type = "subscribers.set", Value = subscribers });
        }

        public IDictionary<string, dynamic> CustomFields
        {
            get
            {
                return new ReadOnlyDictionary<string, dynamic>(this.customFields);
            }
        }
    }
}