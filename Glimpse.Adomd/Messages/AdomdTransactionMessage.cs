namespace Glimpse.Adomd.Messages
{
    using System;
    
    /// <summary>
    /// Base class for transaction related messages.
    /// </summary>
    public abstract class AdomdTransactionMessage : AdomdMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdomdTransactionMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        protected AdomdTransactionMessage(Guid connectionId, Guid transactionId)
            : base(connectionId)
        {
            TransactionId = transactionId;
        }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        public Guid TransactionId { get; protected set; }
    }
}
