//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// <see cref="EnumExtensions"/>
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets all attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllAttributes<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            object[] attributes = null;

            if (memberInfo.Length > 0)
            {
                attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            }

            return attributes as IEnumerable<T>;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The attribute</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            object[] attributes = null;

            if (memberInfo.Length > 0)
            {
                attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            }

            return attributes != null && attributes.Length > 0 ? attributes[0] as T : null;
        }
    }
}