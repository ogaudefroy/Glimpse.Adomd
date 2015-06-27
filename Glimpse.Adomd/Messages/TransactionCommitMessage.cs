namespace Glimpse.Adomd.Messages
{
    using System;

    /// <summary>
    /// Message published when transaction has been commited.
    /// </summary>
    public class TransactionCommitMessage : AdomdTransactionMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCommitMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        public TransactionCommitMessage(Guid connectionId, Guid transactionId)
            : base(connectionId, transactionId)
        {
        }
    }
}
