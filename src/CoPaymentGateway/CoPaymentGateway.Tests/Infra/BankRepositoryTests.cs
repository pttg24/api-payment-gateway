//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Tests.Infra
{
    using System;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.BankAggregate;
    using CoPaymentGateway.Infrastructure.Repositories;
    using CoPaymentGateway.Infrastructure.Services;

    using Moq;

    using Xunit;

    /// <summary>
    /// <see cref="BankRepositoryTests"/>
    /// </summary>
    public class BankRepositoryTests
    {
        private readonly Mock<IFakeBankResponseService> bankResponseService;

        public BankRepositoryTests()
        {
            this.bankResponseService = new Mock<IFakeBankResponseService>();
        }

        /// <summary>
        /// Accepts the request.
        /// </summary>
        [Fact]
        public async Task AcceptRequest()
        {
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

            var approvedStatus = 1;

            var fakeBankResponse = new BankPaymentResponse()
            {
                BankId = 1,
                PaymentId = Guid.NewGuid(),
                Status = approvedStatus
            };

            //Act
            this.bankResponseService.Setup(x => x.RandomResponseGenerator()).Returns(Task.FromResult(fakeBankResponse));

            var repo = new BankRepository(this.bankResponseService.Object);
            var result = repo.ProcessPayment(fakePaymentRequest).Result;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(fakeBankResponse.Status, result.Status);
            Assert.Equal(fakeBankResponse.PaymentId, result.PaymentId);
        }

        /// <summary>
        /// Rejects the request.
        /// </summary>
        [Fact]
        public async Task RejectRequest()
        {
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

            var rejectedStatus = 2;

            var fakeBankResponse = new BankPaymentResponse()
            {
                BankId = 1,
                PaymentId = Guid.NewGuid(),
                Status = rejectedStatus
            };

            //Act
            this.bankResponseService.Setup(x => x.RandomResponseGenerator()).Returns(Task.FromResult(fakeBankResponse));

            var repo = new BankRepository(this.bankResponseService.Object);
            var result = repo.ProcessPayment(fakePaymentRequest).Result;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(fakeBankResponse.Status, result.Status);
            Assert.Equal(fakeBankResponse.PaymentId, result.PaymentId);
        }
    }
}