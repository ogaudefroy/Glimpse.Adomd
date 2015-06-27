namespace Glimpse.Adomd.AlternateType
{
    using System;
    using System.Data;
    using Messages;

    /// <summary>
    /// IDbTransaction's implementation which enables Glimpse's instrumentation.
    /// </summary>
    public class GlimpseAdomdTransaction : IDbTransaction
    {
        private readonly IDbTransaction _innerTransaction;
        private readonly GlimpseAdomdConnection _innerConnection;
        private readonly ITimedMessagePublisher _messagePublisher;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdTransaction"/> class.
        /// </summary>
        /// <param name="adomdTransaction">The underlying IDbTransaction.</param>
        /// <param name="connection">The associated GlimpseAdomdConnection.</param>
        public GlimpseAdomdTransaction(IDbTransaction adomdTransaction, GlimpseAdomdConnection connection)
        {
            if (adomdTransaction == null)
            {
                throw new ArgumentNullException("adomdTransaction");
            }
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _innerTransaction = adomdTransaction;
            _innerConnection = connection;
            TransactionId = Guid.NewGuid();

            _messagePublisher = new TimedMessagePublisher();
            _messagePublisher.EmitStartMessage(
                new TransactionBeganMessage(
                    _innerConnection.ConnectionId, 
                    TransactionId, 
                    _innerTransaction.IsolationLevel));
        }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        public Guid TransactionId { get; private set; }

        /// <inheritdoc />
        public void Commit()
        {
            _innerTransaction.Commit();
            _messagePublisher.EmitStopMessage(
                new TransactionCommitMessage(
                    _innerConnection.ConnectionId, 
                    TransactionId));
        }

        /// <inheritdoc />
        public IDbConnection Connection
        {
            get { return _innerConnection; }
        }

        /// <inheritdoc />
        public IsolationLevel IsolationLevel
        {
            get { return _innerTransaction.IsolationLevel; }
        }

        /// <inheritdoc />
        public void Rollback()
        {
            _innerTransaction.Rollback();
            _messagePublisher.EmitStopMessage(
                new TransactionRollbackMessage(
                    _innerConnection.ConnectionId, 
                    TransactionId));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _innerTransaction.Dispose();
        }
    }
}
