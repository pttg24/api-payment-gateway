//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain
{
    /// <summary>
    /// <see cref="StatusTypes"/>
    /// </summary>
    public enum StatusTypes
    {
        /// <summary>
        /// The created
        /// </summary>
        Created = 0,

        /// <summary>
        /// The approved
        /// </summary>
        Approved = 1,

        /// <summary>
        /// The rejected
        /// </summary>
        Rejected = 2,
    }
}