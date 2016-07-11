﻿namespace Stwalkerster.SharphConduit.Applications.Paste
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Stwalkerster.SharphConduit;
    using Stwalkerster.SharphConduit.Utility;

    /// <summary>
    /// The paste item.
    /// </summary>
    [DebuggerDisplay("P{Identifier} {title}")]
    public class PasteItem : TransactionalObject<int>
    {
        #region Fields

        /// <summary>
        /// The edit policy.
        /// </summary>
        private readonly string editPolicy;

        /// <summary>
        /// The language.
        /// </summary>
        private readonly string language;

        /// <summary>
        /// The project phids.
        /// </summary>
        private readonly List<string> projectPHIDs;

        /// <summary>
        /// The space.
        /// </summary>
        private readonly string space;

        /// <summary>
        /// The status.
        /// </summary>
        private readonly string status;

        /// <summary>
        /// The subscriber phids.
        /// </summary>
        private readonly List<string> subscriberPHIDs;

        /// <summary>
        /// The text.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// The title.
        /// </summary>
        private readonly string title;

        /// <summary>
        /// The view policy.
        /// </summary>
        private readonly string viewPolicy;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PasteItem"/> class.
        /// </summary>
        public PasteItem()
        {
            this.projectPHIDs = new List<string>();
            this.subscriberPHIDs = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PasteItem"/> class.
        /// </summary>
        /// <param name="phid">
        /// The phid.
        /// </param>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="language">
        /// The language.
        /// </param>
        /// <param name="space">
        /// The space.
        /// </param>
        /// <param name="viewPolicy">
        /// The view policy.
        /// </param>
        /// <param name="editPolicy">
        /// The edit policy.
        /// </param>
        /// <param name="dateCreated">
        /// The date created.
        /// </param>
        /// <param name="dateModified">
        /// The date modified.
        /// </param>
        /// <param name="projectPHIDs">
        /// The project phi ds.
        /// </param>
        /// <param name="subscriberPHIDs">
        /// The subscriber phi ds.
        /// </param>
        internal PasteItem(
            string phid,
            int identifier,
            string title,
            string text,
            string status,
            string language,
            string space,
            string viewPolicy,
            string editPolicy,
            int dateCreated,
            int dateModified,
            IEnumerable<string> projectPHIDs,
            IEnumerable<string> subscriberPHIDs)
            : base(dateCreated, dateModified)
        {
            this.ObjectPHID = phid;
            this.Identifier = identifier;

            this.title = title;
            this.text = text;
            this.status = status;
            this.language = language;
            this.space = space;
            this.viewPolicy = viewPolicy;
            this.editPolicy = editPolicy;

            this.projectPHIDs = projectPHIDs.ToList();
            this.subscriberPHIDs = subscriberPHIDs.ToList();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the edit policy.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language
        {
            get
            {
                return this.GetValue("language", this.language);
            }

            set
            {
                this.SetValue(value, "language", this.language);
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IEnumerable<string> Projects
        {
            get
            {
                var enumerable = new List<string>(this.projectPHIDs);

                // TODO: make this reflect the pending transactions
                return enumerable;
            }
        }

        /// <summary>
        /// Gets or sets the space.
        /// </summary>
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
        /// Gets or sets the status.
        /// </summary>
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
        /// Gets the subscribers.
        /// </summary>
        public IEnumerable<string> Subscribers
        {
            get
            {
                var enumerable = new List<string>(this.subscriberPHIDs);

                // TODO: make this reflect the pending transactions
                return enumerable;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.GetValue("text", this.text);
            }

            set
            {
                this.SetValue(value, "text", this.text);
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the view policy.
        /// </summary>
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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add comment.
        /// </summary>
        /// <param name="commentText">
        /// The text.
        /// </param>
        public void AddComment(string commentText)
        {
            this.PendingTransactions.Add(
                RandomProvider.Next().ToString(),
                new Transaction { Type = "comment", Value = commentText });
        }

        #endregion
    }
}