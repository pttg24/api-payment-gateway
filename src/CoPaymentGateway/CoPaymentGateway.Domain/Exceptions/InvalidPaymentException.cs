//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.Exceptions
{
    using System;

    /// <summary>
    ///   <see cref="InvalidPaymentException" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidPaymentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPaymentException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPaymentException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPaymentException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InvalidPaymentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}