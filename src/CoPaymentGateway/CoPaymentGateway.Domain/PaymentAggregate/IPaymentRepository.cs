//-----------------------------------------------------------------------
// <copyright file="IPaymentRepository.cs" company="CofcoIntl, Lda">
//     Copyright (c) CofcoIntl, Lda. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.PaymentAggregate
{
    using System;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain.BankAggregate;

    /// <summary>
    /// <see cref="IPaymentRepository"/>
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="requestPayment">The request payment.</param>
        /// <returns></returns>
        Task<PaymentResponse> GetPaymentAsync(Guid internalPaymentId);

        /// <summary>
        /// Inserts the payment asynchronous.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns></returns>
        Task<Guid> InsertPaymentAsync(PaymentRequest paymentRequest);

        /// <summary>
        /// Updates the internal payment asynchronous.
        /// </summary>
        /// <param name="internalPaymentId">The internal payment identifier.</param>
        /// <param name="bankPaymentResponse">The bank payment response.</param>
        /// <returns></returns>
        Task UpdateInternalPaymentAsync(Guid internalPaymentId, BankPaymentResponse bankPaymentResponse);
    }
}