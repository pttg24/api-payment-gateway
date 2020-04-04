//-----------------------------------------------------------------------
// <copyright file="GetPaymentQuery.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.CQRS.Queries
{
    using System;

    using MediatR;

    /// <summary>
    /// <see cref="GetPaymentQuery"/>
    /// </summary>
    public class GetPaymentQuery : IRequest<string>
    {
        /// <summary>
        /// The payment identifier
        /// </summary>
        public readonly Guid PaymentId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentQuery"/> class.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        public GetPaymentQuery(Guid paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}