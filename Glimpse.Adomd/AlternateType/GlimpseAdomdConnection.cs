namespace Glimpse.Adomd.AlternateType
{
    using System;
    using System.Data;
    using Core.Message;
    using Messages;
    using Microsoft.AnalysisServices.AdomdClient;

    /// <summary>
    /// IAdomdConnection's wrapper which implements glimpse instrumentation.
    /// </summary>
    public class GlimpseAdomdConnection : IAdomdConnection
    {
        private readonly IAdomdConnection _innerConnection;
        private readonly Guid _connectionId;
        private readonly ITimedMessagePublisher _messageEmitter;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public GlimpseAdomdConnection(string connectionString)
            : this(new AdomdConnection(connectionString))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdConnection"/> class.
        /// </summary>
        /// <param name="connection">The underlying unwrapped adomdconnection.</param>
        public GlimpseAdomdConnection(AdomdConnection connection)
            : this(new AdomdConnectionWrapper(connection))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdConnection"/> class.
        /// </summary>
        /// <param name="connection">An already wrapped adomdconnection.</param>
        public GlimpseAdomdConnection(IAdomdConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _innerConnection = connection;
            _connectionId = Guid.NewGuid();
            _messageEmitter = new TimedMessagePublisher();
        }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        public Guid ConnectionId
        {
            get { return _connectionId; }
        }

        /// <inheritdoc />
        public string ConnectionString
        {
            get { return _innerConnection.ConnectionString; }
            set { _innerConnection.ConnectionString = value; }
        }

        /// <inheritdoc />
        public int ConnectionTimeout
        {
            get { return _innerConnection.ConnectionTimeout; }
        }

        /// <inheritdoc />
        public string Database
        {
            get { return _innerConnection.Database; }
        }

        /// <inheritdoc />
        public ConnectionState State
        {
            get { return _innerConnection.State; }
        }

        /// <inheritdoc />
        public CubeCollection Cubes
        {
            get { return _innerConnection.Cubes; }
        }

        /// <inheritdoc />
        public MiningModelCollection MiningModels
        {
            get { return _innerConnection.MiningModels; }
        }

        /// <inheritdoc />
        public MiningStructureCollection MiningStructures
        {
            get { return _innerConnection.MiningStructures; }
        }

        /// <inheritdoc />
        public MiningServiceCollection MiningServices
        {
            get { return _innerConnection.MiningServices; }
        }

        /// <inheritdoc />
        public string ProviderVersion
        {
            get { return _innerConnection.ProviderVersion; }
        }

        /// <inheritdoc />
        public string ClientVersion
        {
            get { return _innerConnection.ClientVersion; }
        }

        /// <inheritdoc />
        public string ServerVersion
        {
            get { return _innerConnection.ServerVersion; }
        }

        /// <inheritdoc />
        public string SessionID
        {
            get { return _innerConnection.SessionID; }
        }

        /// <inheritdoc />
        public bool ShowHiddenObjects
        {
            get { return _innerConnection.ShowHiddenObjects; }
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction()
        {
            return new GlimpseAdomdTransaction(_innerConnection.BeginTransaction(), this);
        }

        /// <inheritdoc />
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return new GlimpseAdomdTransaction(_innerConnection.BeginTransaction(il), this);
        }

        /// <inheritdoc />
        public void ChangeDatabase(string databaseName)
        {
            _innerConnection.ChangeDatabase(databaseName);
        }

        /// <inheritdoc />
        public IAdomdCommand CreateCommand()
        {
            return new GlimpseAdomdCommand(_innerConnection.CreateCommand(), this, this.ConnectionId);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_innerConnection.State != ConnectionState.Closed)
            {
                Close();
            }
            _innerConnection.Dispose();
        }

        /// <inheritdoc />
        public void Open()
        {
            _messageEmitter.EmitStartMessage(new ConnectionStartedMessage(_connectionId));
            _innerConnection.Open();
        }

        /// <inheritdoc />
        public void Close()
        {
            _messageEmitter.EmitStopMessage(new ConnectionClosedMessage(_connectionId).AsTimelineMessage("Connection: Opened", AdomdTimelineCategory.Connection));
            _innerConnection.Close();
        }

        /// <inheritdoc />
        IDbCommand IDbConnection.CreateCommand()
        {
            return new GlimpseAdomdCommand(_innerConnection.CreateCommand(), this, this.ConnectionId);
        }

        /// <inheritdoc />
        public void Close(bool endSession)
        {
            _messageEmitter.EmitStopMessage(new ConnectionClosedMessage(_connectionId).AsTimelineMessage("Connection: Opened", AdomdTimelineCategory.Connection));
            _innerConnection.Close(endSession);
        }

        /// <inheritdoc />
        public DataSet GetSchemaDataSet(Guid schema, object[] restrictions)
        {
            return _innerConnection.GetSchemaDataSet(schema, restrictions);
        }

        /// <inheritdoc />
        public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions)
        {
            return _innerConnection.GetSchemaDataSet(schemaName, restrictions);
        }

        /// <inheritdoc />
        public DataSet GetSchemaDataSet(Guid schema, object[] restrictions, bool throwOnInlineErrors)
        {
            return _innerConnection.GetSchemaDataSet(schema, restrictions, throwOnInlineErrors);
        }

        /// <inheritdoc />
        public DataSet GetSchemaDataSet(string schema, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
        {
            return _innerConnection.GetSchemaDataSet(schema, restrictions, throwOnInlineErrors);
        }

        /// <inheritdoc />
        public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions)
        {
            return _innerConnection.GetSchemaDataSet(schemaName, schemaNamespace, restrictions);
        }

        /// <inheritdoc />
        public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
        {
            return _innerConnection.GetSchemaDataSet(schemaName, schemaNamespace, restrictions, throwOnInlineErrors);
        }

        /// <inheritdoc />
        public void RefreshMetadata()
        {
            _innerConnection.RefreshMetadata();
        }
    }
}
