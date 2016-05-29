namespace Stwalkerster.ConduitClient.Applications
{
    using System;

    /// <summary>
    /// Represents a task in Maniphest
    /// </summary>
    /// <remarks>
    /// TODO: column
    /// TODO: space
    /// TODO: points
    /// TODO: view + edit
    /// TODO: projects (add, remove, set)
    /// TODO: subscribers (add, remove, set)
    /// </remarks>
    public class ManiphestTask : TransactionalObject
    {
        private readonly string title;

        private readonly string description;

        private readonly string status;

        private readonly string parent;

        private readonly string priority;

        private readonly string owner;

        public ManiphestTask()
        {
        }

        internal ManiphestTask(string phid, string title, string description, string status, string parent,string priority, string owner)
        {
            this.ObjectPHID = phid;
            this.title = title;
            this.description = description;
            this.status = status;
            this.parent = parent;
            this.priority = priority;
            this.owner = owner;
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

        /// <summary>
        /// TODO: PHID
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
        /// TODO: PHID/String/null
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
        
        public void AddComment(string text)
        {
            this.PendingTransactions.Add(new Guid().ToString(), new Transaction { Type = "comment", Value = text });
        }
    }
}
