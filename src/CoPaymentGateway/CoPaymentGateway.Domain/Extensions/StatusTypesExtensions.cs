//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.Extensions
{
    using System;
    using System.Collections.Concurrent;

    using CoPaymentGateway.Domain.Attributes;

    /// <summary>
    /// <see cref="StatusTypesExtensions"/>
    /// </summary>
    public static class StatusTypesExtensions
    {
        /// <summary>
        /// The enum attribute values
        /// </summary>
        private static ConcurrentDictionary<StatusTypes, Tuple<string, string>> enumAttributeValues;

        /// <summary>
        /// Initializes the <see cref="FileStatusTypesExtensions"/> class.
        /// </summary>
        static StatusTypesExtensions()
        {
            StatusTypesExtensions.enumAttributeValues = new ConcurrentDictionary<StatusTypes, Tuple<string, string>>();
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns>the enum attribute description.</returns>
        public static string GetDescription(this StatusTypes enumValue)
        {
            return StatusTypesExtensions.enumAttributeValues.GetOrAdd(enumValue, e =>
            {
                return StatusTypesExtensions.GetTuple(e);
            }).Item2;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public static string GetName(this StatusTypes enumValue)
        {
            return enumAttributeValues.GetOrAdd(enumValue, e =>
            {
                return GetTuple(e);
            }).Item1;
        }

        /// <summary>
        /// Gets the tuple.
        /// </summary>
        /// <param name="enumContexts">The enum value.</param>
        /// <returns></returns>
        private static Tuple<string, string> GetTuple(StatusTypes enumContexts)
        {
            var attributeName = enumContexts.GetAttribute<NameAttribute>();
            var attributeDesc = enumContexts.GetAttribute<DescriptionAttribute>();
            return new Tuple<string, string>(attributeName == null ? enumContexts.ToString() : attributeName.Name, attributeDesc == null ? enumContexts.ToString() : attributeDesc.Description);
        }
    }
}