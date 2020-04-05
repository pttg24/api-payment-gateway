//-----------------------------------------------------------------------
// <copyright>
//     Author: Pedro Tiago Gomes, 2020
// </copyright>
//-----------------------------------------------------------------------

namespace CoPaymentGateway.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// <see cref="UnitOfWork"/>
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed;

        public UnitOfWork(PaymentGatewayContext context)
        {
            this.disposed = false;
            this.Context = context;
        }

        public PaymentGatewayContext Context { get; }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            this.disposed = true;
        }
    }
}