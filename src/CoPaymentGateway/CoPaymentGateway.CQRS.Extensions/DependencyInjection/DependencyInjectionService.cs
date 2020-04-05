//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.CQRS.Extensions.DependencyInjection
{
    using CoPaymentGateway.CQRS.Commands;
    using CoPaymentGateway.CQRS.Queries;

    using MediatR;

    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="DependencyInjectionService"/>
    /// </summary>
    public static class DependencyInjectionService
    {
        /// <summary>
        /// Adds the CQRS commands.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCQRSCommands(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ProcessPaymentCommand).Assembly);

            return services;
        }

        /// <summary>
        /// Adds the CQRS queries.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCQRSQueries(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetPaymentQuery).Assembly);

            return services;
        }
    }
}