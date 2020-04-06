//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Tests.API
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using CoPaymentGateway.Controllers;
    using CoPaymentGateway.CQRS.Commands;
    using CoPaymentGateway.CQRS.Queries;
    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.Extensions;

    using MediatR;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Moq;

    using Xunit;

    /// <summary>
    /// <see cref="PaymentsControllerTests"/>
    /// </summary>
    public class PaymentsControllerTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentsControllerTests"/> class.
        /// </summary>
        public PaymentsControllerTests()
        {
            this.mediator = new Mock<IMediator>();
        }

        private Mock<IMediator> mediator { get; set; }

        /// <summary>
        /// Creates the payment.
        /// </summary>
        [Fact]
        public async Task CreatePayment()
        {
            var mock = new Mock<ILogger<PaymentsController>>();
            ILogger<PaymentsController> logger = mock.Object;

            //Arrange
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

            var id = Guid.NewGuid();

            this.mediator
            .Setup(m => m.Send(It.IsAny<ProcessPaymentCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(id)
            .Verifiable("Guid was not sent.");

            var controller = new PaymentsController(this.mediator.Object, logger);

            //Act
            var response = controller.PostPayment(fakePaymentRequest);

            //Assert
            Assert.IsType<OkObjectResult>(response.Result);

            var okObjectResult = response.Result as OkObjectResult;
            Assert.Equal(id, okObjectResult.Value);
        }

        /// <summary>
        /// Gets the payment return200.
        /// </summary>
        [Fact]
        public async Task GetPaymentReturn200()
        {
            //Arrange
            var mock = new Mock<ILogger<PaymentsController>>();
            ILogger<PaymentsController> logger = mock.Object;

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

            var id = Guid.NewGuid();

            this.mediator
            .Setup(m => m.Send(It.IsAny<ProcessPaymentCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(id)
            .Verifiable("Guid was not sent.");

            this.mediator
            .Setup(m => m.Send(It.IsAny<GetPaymentQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakePaymentResponse)
            .Verifiable("Guid was not returned.");

            var controller = new PaymentsController(this.mediator.Object, logger);

            //Act
            //simulate Post
            var responseFromPost = controller.PostPayment(fakePaymentRequest);

            var okObjectResult = responseFromPost.Result as OkObjectResult;

            Guid internalIdReturnedFromPost = (Guid)okObjectResult.Value;

            //simulate Get
            var responseFromGet = controller.GetPayment(internalIdReturnedFromPost);

            // Assert
            Assert.IsType<OkObjectResult>(responseFromGet.Result);
        }

        /// <summary>
        /// Gets the payment return404.
        /// </summary>
        [Fact]
        public async Task GetPaymentReturn404()
        {
            var mock = new Mock<ILogger<PaymentsController>>();
            ILogger<PaymentsController> logger = mock.Object;

            // Arrange
            var controller = new PaymentsController(this.mediator.Object, logger);

            // Act
            var response = controller.GetPayment(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }
    }
}