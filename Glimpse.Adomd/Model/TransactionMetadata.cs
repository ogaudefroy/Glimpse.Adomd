namespace Glimpse.Adomd.Model
{
    using System;

    /// <summary>
    /// Metadata for ADOMD transactions.
    /// </summary>
    public class TransactionMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionMetadata"/> class.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="connectionId">The connection identifier.</param>
        public TransactionMetadata(string transactionId, string connectionId)
        {
            Id = transactionId;
            ConnectionId = connectionId;
        }

        /// <summary>
        /// Gets the transaction id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the connection id.
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Gets or sets the transaction isolation level.
        /// </summary>
        public string IsolationLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the transaction has been commited.
        /// </summary>
        public bool? Committed { get; set; }

        /// <summary>
        /// Gets or sets the transaction start time.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the transaction end time.
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the transaction duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the transaction offset.
        /// </summary>
        public TimeSpan? Offset { get; set; }
    }
}
