// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConduitApplicationBase.cs" company="">
//   
// </copyright>
// <summary>
//   The conduit application base.
// </summary>
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
        protected ConduitClient client;

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
            get
            {
                return this.client;
            }
        }

        #endregion
    }
}
