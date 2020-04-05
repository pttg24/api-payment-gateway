//-----------------------------------------------------------------------
// <copyright file="IBankRepository.cs" company="CofcoIntl, Lda">
//     Copyright (c) CofcoIntl, Lda. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading.Tasks;

namespace CoPaymentGateway.Domain.BankAggregate
{
    /// <summary>
    /// <see cref="IBankRepository"/>
    /// </summary>
    public interface IBankRepository
    {
        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns></returns>
        Task<BankPaymentResponse> ProcessPayment(PaymentRequest paymentRequest);
    }
}