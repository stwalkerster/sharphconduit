namespace Stwalkerster.SharphConduit.Applications.Projects
{
    /// <summary>
    /// The workboard column.
    /// </summary>
    public class WorkboardColumn : TransactionalObject<int>
    {
        /// <summary>
        /// The name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The project.
        /// </summary>
        private readonly string project;

        /// <summary>
        /// The view policy.
        /// </summary>
        private readonly string viewPolicy;

        /// <summary>
        /// The edit policy.
        /// </summary>
        private readonly string editPolicy;

        /// <summary>
        /// The proxy phid.
        /// </summary>
        private readonly string proxyPHID;

        // public WorkboardColumn()
        // {
        // }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkboardColumn"/> class.
        /// </summary>
        /// <param name="phid">
        /// The phid.
        /// </param>
        /// <param name="identifier">
        /// The identifier.
        /// </param>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="project">
        /// The project.
        /// </param>
        /// <param name="viewPolicy">
        /// The view policy.
        /// </param>
        /// <param name="editPolicy">
        /// The edit policy.
        /// </param>
        /// <param name="proxyPHID">
        /// The proxy phid.
        /// </param>
        /// <param name="dateCreated">
        /// The date created.
        /// </param>
        /// <param name="dateModified">
        /// The date modified.
        /// </param>
        public WorkboardColumn(
            string phid,
            int identifier,
            string uri,
            string name,
            string project,
            string viewPolicy,
            string editPolicy,
            string proxyPHID,
            int dateCreated,
            int dateModified)
            : base(dateCreated, dateModified)
        {
            this.ObjectPHID = phid;
            this.Identifier = identifier;
            this.Uri = uri;

            this.name = name;
            this.project = project;

            this.proxyPHID = proxyPHID;

            this.viewPolicy = viewPolicy;
            this.editPolicy = editPolicy;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValue("name", this.name);
            }

            // set { this.SetValue(value, "name", this.name); }
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public string Project
        {
            get
            {
                return this.GetValue("project", this.project);
            }

            // set { this.SetValue(value, "project", this.project); }
        }

        /// <summary>
        /// Gets the proxy phid.
        /// </summary>
        public string ProxyPHID
        {
            get
            {
                return this.GetValue("proxy", this.proxyPHID);
            }

            // set { this.SetValue(value, "proxy", this.proxyPHID); }
        }

        /// <summary>
        /// Gets the view policy.
        /// </summary>
        public string ViewPolicy
        {
            get
            {
                return this.GetValue("view", this.viewPolicy);
            }

            // set { this.SetValue(value, "view", this.viewPolicy); }
        }

        /// <summary>
        /// Gets the edit policy.
        /// </summary>
        public string EditPolicy
        {
            get
            {
                return this.GetValue("edit", this.editPolicy);
            }

            // set { this.SetValue(value, "edit", this.editPolicy); }
        }
    }
}