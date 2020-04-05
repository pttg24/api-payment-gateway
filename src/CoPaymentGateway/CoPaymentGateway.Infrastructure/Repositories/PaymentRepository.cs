//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CoPaymentGateway.Domain;
    using CoPaymentGateway.Domain.BankAggregate;
    using CoPaymentGateway.Domain.Exceptions;
    using CoPaymentGateway.Domain.PaymentAggregate;
    using CoPaymentGateway.Infrastructure.DataModels;

    /// <summary>
    /// <see cref="PaymentRepository"/>
    /// </summary>
    public class PaymentRepository : IPaymentRepository, IDisposable
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly PaymentGatewayContext _context;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// The dispoed
        /// </summary>
        private bool disposed;

        public PaymentRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="internalPaymentId"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Payment not found</exception>
        public async Task<PaymentResponse> GetPaymentAsync(Guid internalPaymentId)
        {
            var internalPaymentInfo = await Task.FromResult(this.unitOfWork.Context.Payments.FirstOrDefault(ip => ip.InternalPaymentId == internalPaymentId));

            if (internalPaymentInfo != null)
            {
                return new PaymentResponse()
                {
                    Amount = internalPaymentInfo.Amount,
                    BankPaymentId = internalPaymentInfo.BankPaymentId,
                    CardCvv = internalPaymentInfo.CardCvv,
                    CardExpiryMonth = internalPaymentInfo.CardExpiryMonth,
                    CardExpiryYear = internalPaymentInfo.CardExpiryYear,
                    CardName = internalPaymentInfo.CardName,
                    CardNumber = internalPaymentInfo.CardNumber,
                    CurrencyCode = internalPaymentInfo.CurrencyCode,
                    Status = internalPaymentInfo.Status
                };
            }
            else
            {
                throw new InvalidPaymentException("Internal Payment Info not found.");
            }
        }

        /// <summary>
        /// Inserts the payment asynchronous.
        /// </summary>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns></returns>
        public async Task<Guid> InsertPaymentAsync(PaymentRequest paymentRequest)
        {
            var paymentModel = new Payments();
            paymentModel.Amount = paymentRequest.Amount;
            paymentModel.BankPaymentId = Guid.NewGuid();
            paymentModel.CardCvv = paymentRequest.CardCvv;
            paymentModel.CardExpiryMonth = paymentRequest.CardExpiryMonth;
            paymentModel.CardExpiryYear = paymentRequest.CardExpiryYear;
            paymentModel.CardName = paymentRequest.CardName;
            paymentModel.CardNumber = paymentRequest.CardNumber;
            paymentModel.CurrencyCode = paymentRequest.CurrencyCode;
            paymentModel.InternalPaymentId = Guid.NewGuid();
            paymentModel.Status = (int)StatusTypes.Created;

            await this.unitOfWork.Context.Payments.AddAsync(paymentModel);
            await this.unitOfWork.CommitAsync();

            //return internal Payment ID - this id will not be updated
            //after bank response - BankPaymentId will be updated, so we will have a pair <InternalId, BankId>
            return paymentModel.InternalPaymentId;
        }

        /// <summary>
        /// Updates the payment asynchronous.
        /// </summary>
        /// <param name="internalPaymentId">The internal payment identifier.</param>
        /// <param name="bankPaymentResponse">The bank payment response.</param>
        /// <exception cref="System.Exception">Could not find internal payment code, contact the administration service</exception>
        public async Task UpdateInternalPaymentAsync(Guid internalPaymentId, BankPaymentResponse bankPaymentResponse)
        {
            var internalPaymentInfo = await Task.FromResult(this.unitOfWork.Context.Payments.FirstOrDefault(ip => ip.InternalPaymentId == internalPaymentId));

            if (internalPaymentInfo != null)
            {
                internalPaymentInfo.BankPaymentId = bankPaymentResponse.PaymentId;
                internalPaymentInfo.Status = bankPaymentResponse.Status;

                this.unitOfWork.Context.Payments.Update(internalPaymentInfo);
                await this.unitOfWork.CommitAsync();
            }
            else
            {
                throw new InvalidPaymentException("Internal Payment Info not found.");
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                this.unitOfWork.Context.Dispose();
            }

            this.disposed = true;
        }
    }
}