﻿namespace Stwalkerster.SharphConduit.Applications.Files
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Stwalkerster.SharphConduit;

    /// <summary>
    /// The files.
    /// </summary>
    public class Files : ConduitApplicationBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConduitApplicationBase"/> class.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        public Files(ConduitClient client)
            : base(client)
        {
        }

        public void Upload(IEnumerable<IUploadable> uploadables)
        {
            // Allocate the files as needed
            IEnumerable<AllocateResult> allocateResults = this.AllocateFiles(uploadables);

            IList<AllocateResult> failures = new List<AllocateResult>();
            IList<AllocateResult> success = new List<AllocateResult>();
            IList<AllocateResult> uploadChunk = new List<AllocateResult>();
            IList<AllocateResult> uploadFile = new List<AllocateResult>();

            foreach (var result in allocateResults)
            {
                if (!result.Upload)
                {
                    // we don't want to perform this upload.
                    if (result.PHID != null)
                    {
                        // Server created the file from existing data.
                        success.Add(result);
                    }
                    else
                    {
                        // We failed - the server refused to accept the file.
                        failures.Add(result);
                    }

                    continue;
                }

                if (result.PHID != null)
                {
                    uploadChunk.Add(result);
                }

                uploadFile.Add(result);
            }

            foreach (var chunk in uploadChunk)
            {
                try
                {
                    this.UploadChunks(chunk);
                    chunk.Uploadable.PHID = chunk.PHID;
                    success.Add(chunk);
                }
                catch (Exception e)
                {
                    chunk.Error = e.Message;
                    failures.Add(chunk);
                }

                uploadFile.Remove(chunk);
            }

            foreach (var file in uploadFile)
            {
                string phid = this.UploadData(file);
                file.Uploadable.PHID = phid;
                
                success.Add(file);
            }
        }

        private void UploadChunks(AllocateResult chunk)
        {
            // Query for chunks
            var chunks = this.ConduitClient.CallMethod(
                "file.querychunks",
                new Dictionary<string, dynamic> { { "filePHID", chunk.PHID } });

            Debugger.Break();
        }

        private string UploadData(AllocateResult file)
        {
            var parameters = new Dictionary<string, dynamic> { { "name", file.Uploadable.Name }, };

            if (file.Uploadable.ViewPolicy != null)
            {
                parameters.Add("viewPolicy", file.Uploadable.ViewPolicy);
            }

            parameters.Add("data_base64", file.Uploadable.GetData());

            dynamic result = this.ConduitClient.CallMethod("file.upload", parameters);

            if (result.error_info != null)
            {
                throw new ConduitException(result.error_code, result.error_info);
            }

            return (string)result.result;
        }



        private IEnumerable<AllocateResult> AllocateFiles(IEnumerable<IUploadable> uploadables)
        {
            List<AllocateResult> results = new List<AllocateResult>();

            foreach (var uploadable in uploadables)
            {
                var parameters = new Dictionary<string, dynamic>
                                     {
                                         { "name", uploadable.Name },
                                         { "contentLength", uploadable.Size },
                                         { "contentHash", uploadable.Hash },
                                     };

                if (uploadable.ViewPolicy != null)
                {
                    parameters.Add("viewPolicy", uploadable.ViewPolicy);
                }

                // TODO: deleteAfterEpoch

                dynamic result = this.ConduitClient.CallMethod("file.allocate", parameters);

                results.Add(new AllocateResult((bool)result.result.upload, (string)result.result.filePHID, uploadable));
            }

            return results;
        }
    }
}
