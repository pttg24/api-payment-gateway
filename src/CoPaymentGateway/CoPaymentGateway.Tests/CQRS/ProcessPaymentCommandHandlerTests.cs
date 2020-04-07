//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Tests.CQRS
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoPaymentGateway.CQRS.Commands;
    using CoPaymentGateway.CQRS.Commands.Handlers;
    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.BankAggregate;
    using CoPaymentGateway.Domain.Exceptions;
    using CoPaymentGateway.Domain.PaymentAggregate;

    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// <see cref="ProcessPaymentCommandHandlerTests"/>
    /// </summary>
    public class ProcessPaymentCommandHandlerTests
    {
        private readonly Mock<IBankRepository> bankRepository;
        private readonly Mock<IPaymentRepository> paymentRepository;

        public ProcessPaymentCommandHandlerTests()
        {
            this.paymentRepository = new Mock<IPaymentRepository>();
            this.bankRepository = new Mock<IBankRepository>();
        }

        /// <summary>
        /// Processes the invalid request.
        /// </summary>
        [Fact]
        public async Task ProcessInvalidRequest()
        {
            //Arrange
            var mock = new Mock<ILogger<ProcessPaymentCommandHandler>>();
            ILogger<ProcessPaymentCommandHandler> logger = mock.Object;

            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "412",
                CardExpiryMonth = 14,
                CardExpiryYear = 2019,
                CardName = "John Doe",
                CardNumber = "Invalid",
                CurrencyCode = "EUR",
            };

            var approvedStatus = 1;

            var fakeBankResponse = new BankPaymentResponse()
            {
                BankId = 1,
                PaymentId = Guid.NewGuid(),
                Status = approvedStatus
            };

            var internalPaymentId = Guid.NewGuid();

            //Act
            this.paymentRepository.Setup(x => x.InsertPaymentAsync(fakePaymentRequest)).Returns(Task.FromResult(internalPaymentId));
            this.bankRepository.Setup(x => x.ProcessPayment(fakePaymentRequest)).Returns(Task.FromResult(fakeBankResponse));
            this.paymentRepository.Setup(x => x.UpdateInternalPaymentAsync(internalPaymentId, fakeBankResponse));

            var command = new ProcessPaymentCommand(fakePaymentRequest);
            var handler = new ProcessPaymentCommandHandler(this.paymentRepository.Object, this.bankRepository.Object, logger);

            Task act() => handler.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<InvalidPaymentException>(act);

            InvalidPaymentException exception;
            exception = await Assert.ThrowsAsync<InvalidPaymentException>(act);
            Assert.Equal("Request properties are wrong", exception.Message);
        }

        /// <summary>
        /// Processes the valid request.
        /// </summary>
        [Fact]
        public async Task ProcessValidRequest()
        {
            //Arrange
            var mock = new Mock<ILogger<ProcessPaymentCommandHandler>>();
            ILogger<ProcessPaymentCommandHandler> logger = mock.Object;

            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "412",
                CardExpiryMonth = 12,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            var approvedStatus = 1;

            var fakeBankResponse = new BankPaymentResponse()
            {
                BankId = 1,
                PaymentId = Guid.NewGuid(),
                Status = approvedStatus
            };

            var internalPaymentId = Guid.NewGuid();

            //Act
            this.paymentRepository.Setup(x => x.InsertPaymentAsync(fakePaymentRequest)).Returns(Task.FromResult(internalPaymentId));
            this.bankRepository.Setup(x => x.ProcessPayment(fakePaymentRequest)).Returns(Task.FromResult(fakeBankResponse));
            this.paymentRepository.Setup(x => x.UpdateInternalPaymentAsync(internalPaymentId, fakeBankResponse));

            var command = new ProcessPaymentCommand(fakePaymentRequest);
            var handler = new ProcessPaymentCommandHandler(this.paymentRepository.Object, this.bankRepository.Object, logger);

            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<Guid>(result);
            Assert.Equal(internalPaymentId, result);
        }
    }
}