﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConduitApplicationBase.cs" company="Simon Walker">
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

namespace Stwalkerster.SharphConduit
{
    /// <summary>
    /// The conduit application base.
    /// </summary>
    public abstract class ConduitApplicationBase
    {
        #region Fields

        /// <summary>
        /// The client.
        /// </summary>
        private readonly ConduitClient client;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConduitApplicationBase"/> class.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        protected ConduitApplicationBase(ConduitClient client)
        {
            this.client = client;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the conduit client.
        /// </summary>
        public ConduitClient ConduitClient
        {
            get { return this.client; }
        }

        #endregion
    }
}