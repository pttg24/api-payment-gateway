//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.Validators
{
    using System;
    using System.Linq;

    using CreditCardValidator;

    using FluentValidation;
    using FluentValidation.Validators;

    /// <summary>
    /// <see cref="PaymentRequestValidator"/>
    /// </summary>
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentRequestValidator"/> class.
        /// </summary>
        public PaymentRequestValidator()
        {
            var year = DateTime.UtcNow.Year;

            this.EnsureInstanceNotNull(this);
            this.RuleFor(r => r.Amount).NotEmpty().GreaterThan(0).NotEmpty();
            this.RuleFor(r => r.CardExpiryMonth).NotEmpty().InclusiveBetween(1, 12);
            this.RuleFor(r => r.CardExpiryYear).NotEmpty().GreaterThanOrEqualTo(year);
            this.RuleFor(r => r.CardName).NotEmpty();
            this.RuleFor(r => r.CardNumber).NotEmpty().SetValidator(new CreditCardChecker());
            this.RuleFor(r => r.CardCvv).Length(3).NotEmpty();
            this.RuleFor(r => r.CurrencyCode).Length(3).NotEmpty();
        }

        /// <summary>
        ///
        /// </summary>
        /// <seealso cref="FluentValidation.Validators.PropertyValidator" />
        private class CreditCardChecker : PropertyValidator
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="CreditCardChecker"/> class.
            /// </summary>
            public CreditCardChecker()
                : base("Invalid Credit Card Number")
            {
            }

            /// <summary>
            /// Returns true if ... is valid.
            /// </summary>
            /// <param name="propValidatorContext">The property validator context.</param>
            /// <returns>
            ///   <c>true</c> if the specified property validator context is valid; otherwise, <c>false</c>.
            /// </returns>
            protected override bool IsValid(PropertyValidatorContext propValidatorContext)
            {
                var creditCardNumber = propValidatorContext.PropertyValue as string;
                if (string.IsNullOrEmpty(creditCardNumber))
                {
                    return false;
                }

                if (!creditCardNumber.All(char.IsDigit))
                {
                    return false;
                }

                var detector = new CreditCardDetector(creditCardNumber);
                return detector.IsValid();
            }
        }
    }
}