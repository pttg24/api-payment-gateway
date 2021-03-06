﻿//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure
{
    using CoPaymentGateway.Infrastructure.DataModels;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// <see cref="PaymentGatewayContext"/>
    /// </summary>
    public partial class PaymentGatewayContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentGatewayContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public PaymentGatewayContext(DbContextOptions<PaymentGatewayContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the payment request.
        /// </summary>
        /// <value>
        /// The payment request.
        /// </value>
        public virtual DbSet<Payments> Payments { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
        }
    }
}