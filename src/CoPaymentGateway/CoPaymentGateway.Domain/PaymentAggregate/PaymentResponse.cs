//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace CoPaymentGateway.Domain
{
    /// <summary>
    /// <see cref="PaymentResponse"/>
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the bank payment identifier.
        /// </summary>
        /// <value>
        /// The bank payment identifier.
        /// </value>
        public Guid BankPaymentId { get; set; }

        /// <summary>
        /// Gets or sets the card CVV.
        /// </summary>
        /// <value>
        /// The card CVV.
        /// </value>
        public string CardCvv { get; set; }

        /// <summary>
        /// Gets or sets the card expiry month.
        /// </summary>
        /// <value>
        /// The card expiry month.
        /// </value>
        public int CardExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the card expiry year.
        /// </summary>
        /// <value>
        /// The card expiry year.
        /// </value>
        public int CardExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets the name of the card.
        /// </summary>
        /// <value>
        /// The name of the card.
        /// </value>
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }
    }
}