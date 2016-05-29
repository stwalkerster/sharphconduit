namespace Stwalkerster.ConduitClient
{
    public class ApplicationEditorSearchConstraint
    {
        private readonly string type;

        private readonly dynamic value;

        public ApplicationEditorSearchConstraint(string type, dynamic value)
        {
            this.type = type;
            this.value = value;
        }

        public string Type
        {
            get
            {
                return this.type;
            }
        }

        public dynamic Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
