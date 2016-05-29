namespace Stwalkerster.ConduitClient
{
    using System;

    public class ConduitException : Exception
    {
        public ConduitException()
        {
        }

        public ConduitException(string errorCode, string errorInfo)
            : base(string.Format("{0}: {1}", errorCode, errorInfo))
        {
        }
    }
}