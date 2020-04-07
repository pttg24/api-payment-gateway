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

    using CoPaymentGateway.CQRS.Queries;
    using CoPaymentGateway.CQRS.Queries.Handlers;
    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.Extensions;
    using CoPaymentGateway.Domain.PaymentAggregate;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using Moq;

    using Xunit;

    /// <summary>
    /// <see cref="GetPaymentQueryHandlerTests"/>
    /// </summary>
    public class GetPaymentQueryHandlerTests
    {
        private readonly Mock<IPaymentRepository> paymentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentQueryHandlerTests"/> class.
        /// </summary>
        public GetPaymentQueryHandlerTests()
        {
            this.paymentRepository = new Mock<IPaymentRepository>();
        }

        /// <summary>
        /// Gets the approved payment.
        /// </summary>
        [Fact]
        public async Task GetApprovedPayment()
        {
            //Arrange
            ILogger<GetPaymentQueryHandler> logger = new Logger<GetPaymentQueryHandler>(new NullLoggerFactory());

            var internalPaymentId = Guid.NewGuid();
            var approvedStatus = 1;
            var fakePaymentResponse = new PaymentResponse()
            {
                Amount = Convert.ToDecimal(17.5),
                BankPaymentId = Guid.NewGuid(),
                CardCvv = "412",
                CardExpiryMonth = 5,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "4566630011223344",
                CurrencyCode = "EUR",
                Status = approvedStatus,
                StatusDesc = ((StatusTypes)approvedStatus).GetName()
            };

            //Act
            this.paymentRepository.Setup(x => x.GetPaymentAsync(internalPaymentId)).Returns(Task.FromResult(fakePaymentResponse));

            var query = new GetPaymentQuery(internalPaymentId);
            var handler = new GetPaymentQueryHandler(this.paymentRepository.Object, logger);
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)StatusTypes.Approved, result.Status);
            Assert.Equal(StatusTypes.Approved.GetName(), result.StatusDesc);
            Assert.Equal(fakePaymentResponse.BankPaymentId, result.BankPaymentId);
        }

        /// <summary>
        /// Gets the rejected payment.
        /// </summary>
        [Fact]
        public async Task GetRejectedPayment()
        {
            //Arrange
            ILogger<GetPaymentQueryHandler> logger = new Logger<GetPaymentQueryHandler>(new NullLoggerFactory());

            var internalPaymentId = Guid.NewGuid();
            var rejectedStatus = 2;
            var fakePaymentResponse = new PaymentResponse()
            {
                Amount = Convert.ToDecimal(17.5),
                BankPaymentId = Guid.NewGuid(),
                CardCvv = "412",
                CardExpiryMonth = 5,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "4566630011223344",
                CurrencyCode = "EUR",
                Status = rejectedStatus,
                StatusDesc = ((StatusTypes)rejectedStatus).GetName()
            };

            //Act
            this.paymentRepository.Setup(x => x.GetPaymentAsync(internalPaymentId)).Returns(Task.FromResult(fakePaymentResponse));

            var query = new GetPaymentQuery(internalPaymentId);
            var handler = new GetPaymentQueryHandler(this.paymentRepository.Object, logger);
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)StatusTypes.Rejected, result.Status);
            Assert.Equal(StatusTypes.Rejected.GetName(), result.StatusDesc);
            Assert.Equal(fakePaymentResponse.BankPaymentId, result.BankPaymentId);
        }
    }
}