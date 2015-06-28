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
        private readonly IDbConnection _innerConnection;
        private readonly Guid _connectionId;
        private readonly ITimedMessagePublisher _messagePublisher;

        private bool _isComplete;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdTransaction"/> class.
        /// </summary>
        /// <param name="innerTransaction">The underlying IDbTransaction.</param>
        /// <param name="connection">The associated AdomdConnection.</param>
        /// <param name="connectionId">The connection identifier.</param>
        public GlimpseAdomdTransaction(IDbTransaction innerTransaction, IDbConnection connection, Guid connectionId)
            : this(innerTransaction, connection, connectionId, new TimedMessagePublisher())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdTransaction"/> class.
        /// </summary>
        /// <param name="innerTransaction">The underlying IDbTransaction.</param>
        /// <param name="connection">The associated AdomdConnection.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="messagePublisher">The message publisher.</param>
        internal GlimpseAdomdTransaction(IDbTransaction innerTransaction, IDbConnection connection, Guid connectionId, ITimedMessagePublisher messagePublisher)
        {
            if (innerTransaction == null)
            {
                throw new ArgumentNullException("innerTransaction");
            }
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (messagePublisher == null)
            {
                throw new ArgumentNullException("messagePublisher");
            }
            _innerTransaction = innerTransaction;
            _innerConnection = connection;
            _connectionId = connectionId;
            _messagePublisher = messagePublisher;

            TransactionId = Guid.NewGuid();
            
            _messagePublisher.EmitStartMessage(
                new TransactionBeganMessage(
                    _connectionId,
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
            _isComplete = true;
            _messagePublisher.EmitStopMessage(
                new TransactionCommitMessage(
                    _connectionId, 
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
                    _connectionId, 
                    TransactionId));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (!_isComplete)
            {
                _messagePublisher.EmitStopMessage(
                new TransactionRollbackMessage(
                    _connectionId,
                    TransactionId));
            }
            _innerTransaction.Dispose();
        }
    }
}
