//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using CoPaymentGateway.CQRS.Commands;
using CoPaymentGateway.CQRS.Queries;
using CoPaymentGateway.Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Prometheus;

namespace CoPaymentGateway.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PaymentsController> logger;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The counter
        /// </summary>
        private Counter getcounter = Metrics.CreateCounter("copaymentgatewaygetcontroller", "Metrics getcounter");

        /// <summary>
        /// The postcounter
        /// </summary>
        private Counter postcounter = Metrics.CreateCounter("copaymentgatewaypostcontroller", "Metrics postcounter");

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentsController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="logger">The logger.</param>
        public PaymentsController(IMediator mediator, ILogger<PaymentsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="paymentId">The payment identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{paymentId}")]
        public async Task<IActionResult> GetPayment(Guid paymentId)
        {
            this.getcounter.Inc();

            this.logger.LogDebug($"Starting GetPayment --> {paymentId}");

            var response = await this.mediator.Send(new GetPaymentQuery(paymentId));

            if (response != null)
            {
                return this.Ok(response);
            }
            else
            {
                return this.NotFound(response);
            }
        }

        /// <summary>
        /// Posts the payment.
        /// </summary>
        /// <param name="requestPaymentAggregate">The request payment aggregate.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostPayment(PaymentRequest requestPaymentAggregate)
        {
            this.postcounter.Inc();

            this.logger.LogInformation($"Starting PostPayment --> Card : {requestPaymentAggregate.CardNumber} Amount {requestPaymentAggregate.Amount} ");

            var internalPaymentRequestId = await this.mediator.Send(new ProcessPaymentCommand(requestPaymentAggregate));

            return this.Ok(internalPaymentRequestId);
        }
    }
}