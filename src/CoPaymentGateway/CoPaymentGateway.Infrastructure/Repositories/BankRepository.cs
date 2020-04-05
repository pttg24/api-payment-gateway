//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure.Repositories
{
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.BankAggregate;
    using CoPaymentGateway.Infrastructure.Services;

    /// <summary>
    /// <see cref="BankRepository"/>
    /// </summary>
    public class BankRepository : IBankRepository
    {
        /// <summary>
        /// The fake bank response service
        /// </summary>
        private readonly IFakeBankResponseService fakeBankResponseService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankRepository"/> class.
        /// </summary>
        public BankRepository(IFakeBankResponseService fakeBankResponseService)
        {
            this.fakeBankResponseService = fakeBankResponseService;
        }

        /// <summary>
        /// Processes the payment.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns></returns>
        public async Task<BankPaymentResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            var response = await this.fakeBankResponseService.RandomResponseGenerator();

            return await Task.FromResult(response);
        }
    }
}