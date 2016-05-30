namespace Stwalkerster.ConduitClient.Applications.Projects
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

        private readonly List<string> memberPHIDs;

        private readonly List<string> watcherPHIDs;

        private readonly string icon;

        private readonly string name;

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

        public List<string> MemberPHIDs
        {
            get
            {
                var enumerable = new List<string>(this.memberPHIDs);
                // TODO: make this reflect the pending transactions
                return enumerable;
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