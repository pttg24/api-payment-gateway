using System;
using System.Threading.Tasks;

using CoPaymentGateway.CQRS.Commands;
using CoPaymentGateway.CQRS.Queries;
using CoPaymentGateway.Domain;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoPaymentGateway.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly ILogger<PaymentsController> _logger;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        public PaymentsController(IMediator mediator)
        {
            this.mediator = mediator;
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
            var response = await this.mediator.Send(new GetPaymentQuery(paymentId));

            return this.Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment(PaymentRequest requestPaymentAggregate)
        {
            var internalPaymentRequestId = await this.mediator.Send(new ProcessPaymentCommand(requestPaymentAggregate));

            return this.Ok(internalPaymentRequestId);
            //return new ObjectResult(entity)
            //{
            //    StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status201Created
            //};
        }
    }
}