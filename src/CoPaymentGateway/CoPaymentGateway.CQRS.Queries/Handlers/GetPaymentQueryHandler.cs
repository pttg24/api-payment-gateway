//-----------------------------------------------------------------------
// <copyright file="GetPaymentQueryHandler.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.CQRS.Queries.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;

    /// <summary>
    /// <see cref="GetPaymentQueryHandler"/>
    /// </summary>
    internal class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, string>
    {
        public GetPaymentQueryHandler()
        {
        }

        public async Task<string> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            return "Payment Gateway";
        }
    }
}