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
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        PaymentGatewayContext Context { get; }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
    }
}