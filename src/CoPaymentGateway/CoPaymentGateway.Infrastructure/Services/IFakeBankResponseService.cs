//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure.Services
{
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain.BankAggregate;

    /// <summary>
    /// <see cref="IFakeBankResponseService"/>
    /// </summary>
    public interface IFakeBankResponseService
    {
        /// <summary>
        /// Randoms the response generator.
        /// </summary>
        /// <returns></returns>
        Task<BankPaymentResponse> RandomResponseGenerator();
    }
}