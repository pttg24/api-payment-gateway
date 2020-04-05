//-----------------------------------------------------------------------
// <copyright file="GetPaymentQueryHandler.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.CQRS.Queries.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.PaymentAggregate;

    using MediatR;

    /// <summary>
    /// <see cref="GetPaymentQueryHandler"/>
    /// </summary>
    internal class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentResponse>
    {
        private readonly IPaymentRepository paymentRepository;

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public async Task<PaymentResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            return await this.paymentRepository.GetPaymentAsync(request.PaymentId);
        }
    }
}