// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileToUpload.cs" company="Simon Walker">
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

using System;
using System.IO;
using Stwalkerster.SharphConduit.Utility;

namespace Stwalkerster.SharphConduit.Applications.Files
{
    /// <summary>
    /// The file to upload.
    /// </summary>
    public class FileToUpload : IUploadable
    {
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

            int bufferSize = length.GetValueOrDefault((int) fileStream.Length - start);
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
            var fileStream = File.Open(this.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return fileStream;
        }

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
            fileStream.Read(buffer, 0, (int) fileStream.Length);

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
    }
}