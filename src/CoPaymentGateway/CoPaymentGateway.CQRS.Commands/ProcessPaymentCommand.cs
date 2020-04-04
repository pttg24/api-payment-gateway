//-----------------------------------------------------------------------
// <copyright file="ProcessPaymentCommand.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using CoPaymentGateway.Domain;

namespace CoPaymentGateway.CQRS.Commands
{
    /// <summary>
    /// <see cref="ProcessPaymentCommand"/>
    /// </summary>
    public class ProcessPaymentCommand
    {
        /// <summary>
        /// The payment request
        /// </summary>
        public readonly PaymentRequest PaymentRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessPaymentCommand"/> class.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        public ProcessPaymentCommand(PaymentRequest paymentRequest)
        {
            this.PaymentRequest = paymentRequest;
        }
    }
}