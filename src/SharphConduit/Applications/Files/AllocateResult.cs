namespace Stwalkerster.SharphConduit.Applications.Files
{
    internal class AllocateResult
    {
        public AllocateResult(bool upload, string phid, IUploadable uploadable)
        {
            this.Upload = upload;
            this.PHID = phid;
            this.Uploadable = uploadable;
        }

        public IUploadable Uploadable { get; private set; }

        public string PHID { get; private set; }

        public bool Upload { get; private set; }

        public string Error { get; set; }
    }
}