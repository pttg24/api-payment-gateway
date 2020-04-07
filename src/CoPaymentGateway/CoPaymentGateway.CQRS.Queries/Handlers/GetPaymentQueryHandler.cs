//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoPaymentGateway.Tests")]

namespace CoPaymentGateway.CQRS.Queries.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.PaymentAggregate;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// <see cref="GetPaymentQueryHandler"/>
    /// </summary>
    internal class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentResponse>
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<GetPaymentQueryHandler> logger;

        private readonly IPaymentRepository paymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentQueryHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository.</param>
        public GetPaymentQueryHandler(IPaymentRepository paymentRepository, ILogger<GetPaymentQueryHandler> logger)
        {
            this.paymentRepository = paymentRepository;
            this.logger = logger;
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
            this.logger.LogDebug($"Starting GetPaymentQuery Handler --> {request.PaymentId}");

            return await this.paymentRepository.GetPaymentAsync(request.PaymentId);
        }
    }
}