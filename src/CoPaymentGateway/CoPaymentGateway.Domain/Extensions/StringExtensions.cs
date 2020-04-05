//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.Extensions
{
    using System;

    /// <summary>
    /// <see cref="StringExtensions"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts to maskedstring.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string ToMaskedString(this string source)
        {
            var charactersToMask = source.Length - 4;
            var maskedCharacters = new String('*', charactersToMask);
            var unMaskedCharacters = source.Substring(charactersToMask, 4);
            return maskedCharacters + unMaskedCharacters;
        }
    }
}