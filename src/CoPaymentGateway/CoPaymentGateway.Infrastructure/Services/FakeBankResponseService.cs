//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure.Services
{
    using System;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain.BankAggregate;

    /// <summary>
    /// <see cref="FakeBankResponseService"/>
    /// </summary>
    public sealed class FakeBankResponseService : IFakeBankResponseService
    {
        /// <summary>
        /// The constant bank identifier
        /// </summary>
        public const int ConstantBankId = 1;

        /// <summary>
        /// Randoms the response generator.
        /// </summary>
        /// <returns></returns>
        public async Task<BankPaymentResponse> RandomResponseGenerator()
        {
            //Approved = 1
            //Rejected = 2
            var status = new Random().Next(1, 3);

            return await Task.FromResult(new BankPaymentResponse()
            {
                Status = status,
                BankId = ConstantBankId,
                PaymentId = Guid.NewGuid()
            });
        }
    }
}