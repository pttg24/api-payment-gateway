//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Helpers
{
    using System;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain.Exceptions;

    using Microsoft.AspNetCore.Http;

    using Newtonsoft.Json;

    using Serilog;

    /// <summary>
    ///
    /// </summary>
    public class ErrorHandlingHelper
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingHelper"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ErrorHandlingHelper(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string details = String.Empty;

            //rewrite exception Code
            var newExceptionCode = HttpStatusCode.InternalServerError;

            switch (ex.GetType().Name)
            {
                case nameof(InvalidPaymentException):
                    newExceptionCode = HttpStatusCode.NotFound;
                    break;
            }

            if (ex.InnerException != null)
            {
                details = ex.InnerException.Message;
            }
            else
            {
                if (stringBuilder.Length > 0)
                {
                    details = stringBuilder.ToString();
                }
            }

            var result = JsonConvert.SerializeObject(new
            {
                error = ex.Message,
                details
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)newExceptionCode;

            Log.Error(ex, details);

            return context.Response.WriteAsync(result);
        }
    }
}