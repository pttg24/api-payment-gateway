//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Domain.Attributes
{
    using System;

    /// <summary>
    /// <see cref="NameAttribute"/>
    /// </summary>
    public class NameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }
    }
}