//-----------------------------------------------------------------------
// <copyright file="ProcessPaymentCommandHandler.cs" company="CofcoIntl, Lda">
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.CQRS.Commands.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain.BankAggregate;
    using CoPaymentGateway.Domain.PaymentAggregate;

    using MediatR;

    /// <summary>
    /// <see cref="ProcessPaymentCommandHandler"/>
    /// </summary>
    internal class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, Guid>
    {
        private readonly IBankRepository bankRepository;
        private readonly IPaymentRepository paymentRepository;

        public ProcessPaymentCommandHandler(IPaymentRepository paymentRepository, IBankRepository bankRepository)
        {
            this.paymentRepository = paymentRepository;
            this.bankRepository = bankRepository;
        }

        public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            //RequestPaymentAggregateValidator validator = new RequestPaymentAggregateValidator();
            //ValidationResult result = validator.Validate(request.RequestPayment);

            //if (!result.IsValid)
            //{
            //    throw new ArgumentValidationException("One or more fields are wrong", result.Errors);
            //}
            //else
            //{
            var internalPaymentId = await this.paymentRepository.InsertPaymentAsync(request.PaymentRequest);
            var bankResponse = await this.bankRepository.ProcessPayment(request.PaymentRequest);

            await this.paymentRepository.UpdateInternalPaymentAsync(internalPaymentId, bankResponse);

            return internalPaymentId;
            //}
        }
    }
}