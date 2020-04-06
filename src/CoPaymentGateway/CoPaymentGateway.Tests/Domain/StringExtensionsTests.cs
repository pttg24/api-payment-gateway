//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Tests.Domain
{
    using CoPaymentGateway.Domain.Extensions;

    using Xunit;

    /// <summary>
    /// <see cref="StringExtensionsTests"/>
    /// </summary>
    public class StringExtensionsTests
    {
        [Fact]
        public void MaskString()
        {
            //Arrange
            var inputString = "1111222233334412";
            var expectedResult = "************4412";

            //Act
            var result = inputString.ToMaskedString();

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}