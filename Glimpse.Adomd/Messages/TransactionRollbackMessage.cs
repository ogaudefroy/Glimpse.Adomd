namespace Glimpse.Adomd.Messages
{
    using System;

    /// <summary>
    /// Message published once transaction has been rolledback.
    /// </summary>
    public class TransactionRollbackMessage : AdomdTransactionMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRollbackMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        public TransactionRollbackMessage(Guid connectionId, Guid transactionId)
            : base(connectionId, transactionId)
        {
        }
    }
}
