namespace Stwalkerster.SharphConduit.Utility
{
    using System;

    using HashLib;

    /// <summary>
    /// The crypto provider.
    /// </summary>
    internal static class CryptoProvider
    {
        /// <summary>
        /// The generate signature.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string CalculateSHA1(this byte[] data)
        {
            IHash sha1 = HashLib.HashFactory.Crypto.CreateSHA1();
            var computeBytes = sha1.ComputeBytes(data);
            return Convert.ToBase64String(computeBytes.GetBytes());
        }
    }
}