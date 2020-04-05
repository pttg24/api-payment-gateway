//-----------------------------------------------------------------------
// <copyright file="BankPaymentResponse.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace CoPaymentGateway.Domain.BankAggregate
{
    /// <summary>
    /// <see cref="BankPaymentResponse"/>
    /// </summary>
    public class BankPaymentResponse
    {
        /// <summary>
        /// Gets or sets the bank identifier.
        /// </summary>
        /// <value>
        /// The bank identifier.
        /// </value>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public Guid PaymentId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }
    }
}