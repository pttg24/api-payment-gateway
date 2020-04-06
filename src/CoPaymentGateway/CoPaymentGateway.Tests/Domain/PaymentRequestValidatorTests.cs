//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Tests.Domain
{
    using System;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.Validators;

    using CreditCardValidator;

    using Xunit;

    /// <summary>
    /// <see cref="PaymentRequestValidatorTests"/>
    /// </summary>
    public class PaymentRequestValidatorTests
    {
        private readonly PaymentRequestValidator paymentRequestValidator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRequestValidatorTests"/> class.
        /// </summary>
        public PaymentRequestValidatorTests()
        {
            this.paymentRequestValidator = new PaymentRequestValidator();
        }

        /// <summary>
        /// Checks the invalid credit card.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        [Theory]
        [InlineData("")]
        [InlineData("Test")]
        public void CheckInvalidCreditCard(string cardNumber)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "123",
                CardExpiryMonth = 1,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = cardNumber,
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);

            CreditCardDetector creditCardDetector;
            Assert.Throws<ArgumentException>(() => creditCardDetector = new CreditCardDetector(""));
        }

        /// <summary>
        /// Checks the invalid CVV.
        /// </summary>
        /// <param name="cvv">The CVV.</param>
        [Theory]
        [InlineData("1")]
        [InlineData("22")]
        [InlineData("4444")]
        public void CheckInvalidCVV(string cvv)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = cvv,
                CardExpiryMonth = 12,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Checks the invalid month.
        /// </summary>
        /// <param name="month">The month.</param>
        [Theory]
        [InlineData(16)]
        [InlineData(0)]
        public void CheckInvalidMonth(int month)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "123",
                CardExpiryMonth = month,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Checks the invalid request.
        /// </summary>
        [Fact]
        public void CheckInvalidRequest()
        {
            //Arrange
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

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Checks the invalid year.
        /// </summary>
        /// <param name="year">The year.</param>
        [Theory]
        [InlineData(900)]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(2019)]
        public void CheckInvalidYear(int year)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "123",
                CardExpiryMonth = 1,
                CardExpiryYear = year,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        /// <summary>
        /// Checks the valid credit card.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        [Theory]
        [InlineData("4659630011223344")]
        [InlineData("1111222233334444")]
        public void CheckValidCreditCard(string cardNumber)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "123",
                CardExpiryMonth = 1,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = cardNumber,
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Checks the valid CVV.
        /// </summary>
        /// <param name="cvv">The CVV.</param>
        [Theory]
        [InlineData("333")]
        public void CheckValidCVV(string cvv)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = cvv,
                CardExpiryMonth = 12,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Checks the valid month.
        /// </summary>
        /// <param name="month">The month.</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public void CheckValidMonth(int month)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "123",
                CardExpiryMonth = month,
                CardExpiryYear = 2020,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Checks the valid request.
        /// </summary>
        [Fact]
        public void CheckValidRequest()
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

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Checks the valid year.
        /// </summary>
        /// <param name="year">The year.</param>
        [Theory]
        [InlineData(2020)]
        public void CheckValidYear(int year)
        {
            //Arrange
            var fakePaymentRequest = new PaymentRequest()
            {
                Amount = Convert.ToDecimal(17.5),
                CardCvv = "123",
                CardExpiryMonth = 1,
                CardExpiryYear = year,
                CardName = "John Doe",
                CardNumber = "1111222233334444",
                CurrencyCode = "EUR",
            };

            //Act
            var result = this.paymentRequestValidator.Validate(fakePaymentRequest);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }
    }
}