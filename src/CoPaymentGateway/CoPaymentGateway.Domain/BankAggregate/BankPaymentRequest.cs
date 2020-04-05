//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace CoPaymentGateway.Domain.BankAggregate
{
    /// <summary>
    /// <see cref="BankPaymentRequest"/>
    /// </summary>
    internal class BankPaymentRequest
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the card CVV.
        /// </summary>
        /// <value>
        /// The card CVV.
        /// </value>
        [Required]
        public string CardCvv { get; set; }

        /// <summary>
        /// Gets or sets the card expiry month.
        /// </summary>
        /// <value>
        /// The card expiry month.
        /// </value>
        [Required]
        public int CardExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the card expiry year.
        /// </summary>
        /// <value>
        /// The card expiry year.
        /// </value>
        [Required]
        public int CardExpiryYear { get; set; }

        /// <summary>
        /// Gets or sets the name of the card.
        /// </summary>
        /// <value>
        /// The name of the card.
        /// </value>
        [Required]
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        /// <value>
        /// The card number.
        /// </value>
        [Required]
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [Required]
        public string CurrencyCode { get; set; }
    }
}