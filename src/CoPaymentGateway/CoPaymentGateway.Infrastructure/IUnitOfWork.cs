//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="CofcoIntl, Lda">
//     Copyright (c) CofcoIntl, Lda. All rights reserved.
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