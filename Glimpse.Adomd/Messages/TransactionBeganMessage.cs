namespace Glimpse.Adomd.Messages
{
    using System;
    using System.Data;

    /// <summary>
    /// Message published once transaction has began.
    /// </summary>
    public class TransactionBeganMessage : AdomdTransactionMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionBeganMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="isolationLevel">The isolation level of the transaction.</param>
        public TransactionBeganMessage(Guid connectionId, Guid transactionId, IsolationLevel isolationLevel)
            : base(connectionId, transactionId)
        {
            TransactionId = transactionId;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// Gets the transaction isolation level.
        /// </summary>
        public IsolationLevel IsolationLevel { get; protected set; }
    }
}
