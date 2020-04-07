//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CoPaymentGateway.Tests")]

namespace CoPaymentGateway.CQRS.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain.BankAggregate;
    using CoPaymentGateway.Domain.Exceptions;
    using CoPaymentGateway.Domain.PaymentAggregate;
    using CoPaymentGateway.Domain.Validators;

    using FluentValidation.Results;

    using MediatR;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// <see cref="ProcessPaymentCommandHandler"/>
    /// </summary>
    internal class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Guid>
    {
        /// <summary>
        /// The bank repository
        /// </summary>
        private readonly IBankRepository bankRepository;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ProcessPaymentCommandHandler> logger;

        /// <summary>
        /// The payment repository
        /// </summary>
        private readonly IPaymentRepository paymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessPaymentCommandHandler"/> class.
        /// </summary>
        /// <param name="paymentRepository">The payment repository.</param>
        /// <param name="bankRepository">The bank repository.</param>
        public ProcessPaymentCommandHandler(IPaymentRepository paymentRepository, IBankRepository bankRepository, ILogger<ProcessPaymentCommandHandler> logger)
        {
            this.paymentRepository = paymentRepository;
            this.bankRepository = bankRepository;
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
        public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                this.logger.LogError($"Starting ProcessPaymentCommand Handler --> request is null --> throwing exception");

                throw new InvalidPaymentException("Request parameter is null");
            }

            PaymentRequestValidator requestChecker = new PaymentRequestValidator();
            ValidationResult requestCheckerResults = requestChecker.Validate(request.PaymentRequest);

            if (!requestCheckerResults.IsValid)
            {
                this.logger.LogError($"Starting ProcessPaymentCommand Handler --> request properties are wrong --> throwing exception");

                throw new InvalidPaymentException("Request properties are wrong");
            }

            var internalPaymentId = await this.paymentRepository.InsertPaymentAsync(request.PaymentRequest);
            var bankResponse = await this.bankRepository.ProcessPayment(request.PaymentRequest);

            this.logger.LogDebug($"Updating Row InternalPaymentId --> {internalPaymentId}");
            this.logger.LogDebug($"Updating Row BankPaymentId --> {bankResponse.PaymentId}");

            await this.paymentRepository.UpdateInternalPaymentAsync(internalPaymentId, bankResponse);

            return internalPaymentId;
        }
    }
}