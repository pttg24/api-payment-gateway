//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure
{
    using System.Threading.Tasks;

    /// <summary>
    /// <see cref="IUnitOfWork"/>
    /// </summary>
    public interface IUnitOfWork
    {
        PaymentGatewayContext Context { get; }

        Task CommitAsync();
    }
}