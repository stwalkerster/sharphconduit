// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileToUpload.cs" company="">
//   
// </copyright>
// <summary>
//   The file to upload.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stwalkerster.SharphConduit.Applications.Files
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using PCLStorage;

    using Stwalkerster.SharphConduit.Utility;

    /// <summary>
    /// The file to upload.
    /// </summary>
    public class FileToUpload : IUploadable
    {
        #region Fields

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToUpload"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public FileToUpload(string path)
        {
            this.Path = path;
            var fileStream = this.OpenFileStream();

            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);

            this.Name = System.IO.Path.GetFileName(path);
            this.Size = 0;
            this.Hash = buffer.CalculateSHA1();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToUpload"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        public FileToUpload(string path, string name) : this(path)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileToUpload"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="viewPolicy">
        /// The view Policy.
        /// </param>
        public FileToUpload(string path, string name, string viewPolicy) : this(path, name)
        {
            this.ViewPolicy = viewPolicy;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the hash.
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public long Size { get; private set; }

        /// <summary>
        /// Gets or sets the phid.
        /// </summary>
        public string PHID { get; set; }

        /// <summary>
        /// Gets the view policy.
        /// </summary>
        public string ViewPolicy { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        #endregion

        /// <summary>
        /// The get data.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetData(int start = 0, int? length = null)
        {
            var fileStream = this.OpenFileStream();
            
            int bufferSize = length.GetValueOrDefault((int)fileStream.Length - start);
            var buf = new byte[bufferSize];

            fileStream.Read(buf, start, bufferSize);

            return Convert.ToBase64String(buf);
        }

        /// <summary>
        /// The open file stream.
        /// </summary>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        private Stream OpenFileStream()
        {
            Task<IFile> fileFromPathTask = FileSystem.Current.GetFileFromPathAsync(this.Path);
            fileFromPathTask.RunSynchronously();
            var file = fileFromPathTask.Result;

            if (file == null)
            {
                throw new Exception("Nonexistent file");
            }

            var openTask = file.OpenAsync(FileAccess.Read);
            openTask.RunSynchronously();

            Stream fileStream = openTask.Result;
            return fileStream;
        }
    }
}