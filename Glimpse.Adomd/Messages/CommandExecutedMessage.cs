namespace Glimpse.Adomd.Messages
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Message published once command has been executed.
    /// </summary>
    public class CommandExecutedMessage : AdomdCommandMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutedMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="commandText">The commmand text.</param>
        /// <param name="parameters">The command parameters.</param>
        /// <param name="hasTransaction">Indicates if the command is included in a transaction.</param>
        public CommandExecutedMessage(Guid connectionId, Guid commandId, string commandText, IList<CommandExecutedParameter> parameters, bool hasTransaction)
              : this(connectionId, commandId, commandText, parameters, hasTransaction, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutedMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="commandText">The commmand text.</param>
        /// <param name="parameters">The command parameters.</param>
        /// <param name="hasTransaction">Indicates if the command is included in a transaction.</param>
        /// <param name="isAsync">Indicates if the command execution is asynchronous.</param>
        public CommandExecutedMessage(Guid connectionId, Guid commandId, string commandText, IList<CommandExecutedParameter> parameters, bool hasTransaction, bool isAsync)
            : base(connectionId, commandId)
        {
            CommandId = commandId;
            CommandText = commandText;
            Parameters = parameters;
            HasTransaction = hasTransaction;
            IsAsync = isAsync;
        }

        /// <summary>
        /// Gets the command text.
        /// </summary>
        public string CommandText { get; protected set; }

        /// <summary>
        /// Gets the command parameters.
        /// </summary>
        public IList<CommandExecutedParameter> Parameters { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the command is included in a transaction.
        /// </summary>
        public bool HasTransaction { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether the command is executed asynchronously.
        /// </summary>
        public bool IsAsync { get; set; }
    }
}
