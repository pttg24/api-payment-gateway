//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using CoPaymentGateway.Domain.Attributes;

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
        [Name("Created")]
        Created = 0,

        /// <summary>
        /// The approved
        /// </summary>
        [Name("Approved")]
        Approved = 1,

        /// <summary>
        /// The rejected
        /// </summary>
        [Name("Rejected")]
        Rejected = 2,
    }
}