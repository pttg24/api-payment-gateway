//-----------------------------------------------------------------------
// <copyright>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository.</param>
        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<PaymentResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            return await this.paymentRepository.GetPaymentAsync(request.PaymentId);
        }
    }
}