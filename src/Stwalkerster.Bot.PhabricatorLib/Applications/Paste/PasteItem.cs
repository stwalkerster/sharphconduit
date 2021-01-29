// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PasteItem.cs" company="Simon Walker">
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

namespace Stwalkerster.Bot.PhabricatorLib.Applications.Paste
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Stwalkerster.Bot.PhabricatorLib;
    using Stwalkerster.Bot.PhabricatorLib.Utility;

    /// <summary>
    /// The paste item.
    /// </summary>
    [DebuggerDisplay("P{Identifier} {title}")]
    public class PasteItem : TransactionalObject<int>
    {
        #region Fields

        /// <summary>
        /// The author phid.
        /// </summary>
        private readonly string authorPHID;

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
        /// <param name="authorPHID">
        /// The author PHID.
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
            string authorPHID,
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
            this.authorPHID = authorPHID;
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
        /// Gets or sets the author phid.
        /// </summary>
        public string AuthorPHID
        {
            get { return this.GetValue("authorPHID", this.authorPHID); }

            set { this.SetValue(value, "authorPHID", this.authorPHID); }
        }

        /// <summary>
        /// Gets or sets the edit policy.
        /// </summary>
        public string EditPolicy
        {
            get { return this.GetValue("edit", this.editPolicy); }

            set { this.SetValue(value, "edit", this.editPolicy); }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language
        {
            get { return this.GetValue("language", this.language); }

            set { this.SetValue(value, "language", this.language); }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IEnumerable<string> Projects
        {
            get
            {
                var enumerable = new List<string>(this.projectPHIDs);

                // TODO: make this reflect the pending transactions - T575
                return enumerable;
            }
        }

        /// <summary>
        /// Gets or sets the space.
        /// </summary>
        public string Space
        {
            get { return this.GetValue("space", this.space); }

            set { this.SetValue(value, "space", this.space); }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status
        {
            get { return this.GetValue("status", this.status); }

            set { this.SetValue(value, "status", this.status); }
        }

        /// <summary>
        /// Gets the subscribers.
        /// </summary>
        public IEnumerable<string> Subscribers
        {
            get
            {
                var enumerable = new List<string>(this.subscriberPHIDs);

                // TODO: make this reflect the pending transactions - T575
                return enumerable;
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get { return this.GetValue("text", this.text); }

            set { this.SetValue(value, "text", this.text); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.GetValue("title", this.title); }

            set { this.SetValue(value, "title", this.title); }
        }

        /// <summary>
        /// Gets or sets the view policy.
        /// </summary>
        public string ViewPolicy
        {
            get { return this.GetValue("view", this.viewPolicy); }

            set { this.SetValue(value, "view", this.viewPolicy); }
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
                new Transaction {Type = "comment", Value = commentText});
        }

        public void AddProjects(string project)
        {
            this.AddProjects(new[] {project});
        }

        public void AddProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add("projects.add", new Transaction {Type = "projects.add", Value = projects});
        }

        public void AddSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.add",
                new Transaction {Type = "subscribers.add", Value = subscribers});
        }

        public void RemoveProjects(string project)
        {
            this.RemoveProjects(new[] {project});
        }

        public void RemoveProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add(
                "projects.remove",
                new Transaction {Type = "projects.remove", Value = projects});
        }

        public void RemoveSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.remove",
                new Transaction {Type = "subscribers.remove", Value = subscribers});
        }

        public void SetProjects(IEnumerable<string> projects)
        {
            this.PendingTransactions.Add("projects.set", new Transaction {Type = "projects.set", Value = projects});
        }

        public void SetSubscribers(IEnumerable<string> subscribers)
        {
            this.PendingTransactions.Add(
                "subscribers.set",
                new Transaction {Type = "subscribers.set", Value = subscribers});
        }

        #endregion
    }
}