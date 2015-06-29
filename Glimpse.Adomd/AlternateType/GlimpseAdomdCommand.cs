namespace Glimpse.Adomd.AlternateType
{
    using System;
    using System.Data;
    using System.Xml;
    using Microsoft.AnalysisServices.AdomdClient;

    /// <summary>
    /// IAdomdCommand's implementation enabling glimpse instrumentation with MDX dumping support.
    /// </summary>
    public class GlimpseAdomdCommand : IAdomdCommand
    {
        private readonly CommandExecutor _commandExecutor;
        private readonly IAdomdCommand _innerCommand;
        private readonly IAdomdConnection _innerConnection;
        private readonly Guid _connectionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GlimpseAdomdCommand"/> class.
        /// </summary>
        /// <param name="command">The wrapped adomdcommand.</param>
        /// <param name="connection">The wrapped adomdconnection.</param>
        /// <param name="connectionId">The Id of its related glimpseadomdconnection.</param>
        public GlimpseAdomdCommand(IAdomdCommand command, IAdomdConnection connection, Guid connectionId)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            _innerCommand = command;
            _innerConnection = connection;
            _connectionId = connectionId;
            _commandExecutor = new CommandExecutor(this);
        }

        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        public Guid CommandId { get; internal set; }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        public Guid ConnectionId
        {
            get { return _connectionId; }
        }

        /// <inheritdoc />
        public string CommandText
        {
            get { return _innerCommand.CommandText; }
            set { _innerCommand.CommandText = value; }
        }

        /// <inheritdoc />
        public int CommandTimeout
        {
            get { return _innerCommand.CommandTimeout; }
            set { _innerCommand.CommandTimeout = value; }
        }

        /// <inheritdoc />
        public CommandType CommandType
        {
            get { return _innerCommand.CommandType; }
            set { _innerCommand.CommandType = value; }
        }

        /// <inheritdoc />
        public IDbConnection Connection
        {
            get { return _innerConnection; }
            set { throw new NotImplementedException("This behavior is not yet implemented."); }
        }

        /// <inheritdoc />
        public IDataParameterCollection Parameters
        {
            get { return _innerCommand.Parameters; }
        }

        /// <inheritdoc />
        public IDbTransaction Transaction
        {
            get { return _innerCommand.Transaction; }
            set { _innerCommand.Transaction = value; }
        }

        /// <inheritdoc />
        public UpdateRowSource UpdatedRowSource
        {
            get { return _innerCommand.UpdatedRowSource; }
            set { _innerCommand.UpdatedRowSource = value; }
        }

        internal IAdomdCommand InnerCommand
        {
            get { return _innerCommand; }
        }

        /// <inheritdoc />
        public void Cancel()
        {
            _innerCommand.Cancel();
        }

        /// <inheritdoc />
        public IDbDataParameter CreateParameter()
        {
            return _innerCommand.CreateParameter();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _innerCommand.Dispose();
        }

        /// <inheritdoc />
        public object Execute()
        {
            return _commandExecutor.Execute((p => p.Execute()), "Execute");
        }

        /// <inheritdoc />
        public XmlReader ExecuteXmlReader()
        {
            return _commandExecutor.Execute(p => p.ExecuteXmlReader(), "ExecuteXmlReader");
        }

        /// <inheritdoc />
        public CellSet ExecuteCellSet()
        {
            return _commandExecutor.Execute(p => p.ExecuteCellSet(), "ExecuteCellSet");
        }

        /// <inheritdoc />
        public int ExecuteNonQuery()
        {
            return _commandExecutor.Execute(p => p.ExecuteNonQuery(), "ExecuteNonQuery");
        }

        /// <inheritdoc />
        public IDataReader ExecuteReader()
        {
            return _commandExecutor.Execute(p => p.ExecuteReader(), "ExecuteReader");
        }

        /// <inheritdoc />
        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            return _commandExecutor.Execute(p => p.ExecuteReader(behavior), "ExecuteReader");
        }

        /// <inheritdoc />
        public object ExecuteScalar()
        {
            return _commandExecutor.Execute(p => p.ExecuteScalar(), "ExecuteScalar");
        }

        /// <inheritdoc />
        public void Prepare()
        {
            _innerCommand.Prepare();
        }
    }
}
