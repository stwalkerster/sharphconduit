// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUploadable.cs" company="">
//   
// </copyright>
// <summary>
//   The Uploadable interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Stwalkerster.SharphConduit.Applications.Files
{
    /// <summary>
    /// The Uploadable interface.
    /// </summary>
    public interface IUploadable
    {
        /// <summary>
        /// Gets the hash.
        /// </summary>
        string Hash { get; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        string Path { get; }

        string PHID { get; set; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        long Size { get; }

        string ViewPolicy { get; }

        string Name { get; }

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
        string GetData(int start = 0, int? length = null);
    }
}
