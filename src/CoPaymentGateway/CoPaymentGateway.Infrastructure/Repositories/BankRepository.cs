//-----------------------------------------------------------------------
// <copyright file="BankRepository.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.BankAggregate;

    /// <summary>
    /// <see cref="BankRepository"/>
    /// </summary>
    public class BankRepository : IBankRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankRepository"/> class.
        /// </summary>
        public BankRepository()
        {
        }

        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns></returns>
        public async Task<BankPaymentResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            return await Task.FromResult(new BankPaymentResponse()
            {
                Status = 1,
                BankId = 1,
                PaymentId = Guid.NewGuid()
            });
        }
    }
}