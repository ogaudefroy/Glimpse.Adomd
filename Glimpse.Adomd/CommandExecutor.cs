namespace Glimpse.Adomd
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using AlternateType;
    using Messages;
    using Microsoft.AnalysisServices.AdomdClient;
    using Core.Message;

    /// <summary>
    /// Component in charge of tracing command execution and publishing appropriate messages.
    /// </summary>
    public class CommandExecutor
    {
        private readonly ITimedMessagePublisher _messagePublisher;
        private readonly GlimpseAdomdCommand _command;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutor"/> class.
        /// </summary>
        /// <param name="command">The glimpse command.</param>
        public CommandExecutor(GlimpseAdomdCommand command)
            : this(command, new TimedMessagePublisher())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExecutor"/> class.
        /// </summary>
        /// <param name="command">The glimpse command.</param>
        /// <param name="messagePublisher">The message publisher.</param>
        public CommandExecutor(GlimpseAdomdCommand command, ITimedMessagePublisher messagePublisher)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (messagePublisher == null)
            {
                throw new ArgumentNullException("messagePublisher");
            }
            _command = command;
            _messagePublisher = messagePublisher;
        }

        /// <summary>
        /// Executes a function on the command and publishes start / stop / error messages.
        /// </summary>
        /// <typeparam name="T">The return type of the function.</typeparam>
        /// <param name="executor">The function to be called on the glimpse command.</param>
        /// <param name="name">Name of the function to be traced.</param>
        /// <returns>The function return value.</returns>
        public T Execute<T>(Func<IAdomdCommand, T> executor, string name)
        {
            T result;
            _command.CommandId = Guid.NewGuid();
            this.LogCommandStart();

            try
            {
                result = executor(_command.InnerCommand);
            }
            catch (Exception exception)
            {
                this.LogCommandError(exception, name);
                throw;
            }

            this.LogCommandEnd(null, name);
            return result;
        }

        /// <summary>
        /// Handles command start execution.
        /// Publishes a CommandExecutedMessage with command text, command parameters.
        /// </summary>
        private void LogCommandStart()
        {
            var parameters = new List<CommandExecutedParameter>();
            if (_command.Parameters != null && _command.Parameters.Count > 0)
            {
                foreach (IDbDataParameter parameter in _command.Parameters)
                {
                    var parameterName = parameter.ParameterName;
                    if (!parameterName.StartsWith("@"))
                    {
                        parameterName = "@" + parameterName;
                    }
                    parameters.Add(new CommandExecutedParameter { Name = parameterName, Value = parameter.Value });
                }
            }
            _messagePublisher.EmitStartMessage(new CommandExecutedMessage(_command.ConnectionId, _command.CommandId, _command.CommandText, parameters, _command.Transaction != null));
        }

        /// <summary>
        /// Handles command end execution.
        /// Publishes a CommandDurationMessage.
        /// </summary>
        /// <param name="recordsAffected">The number of records affected.</param>
        /// <param name="name">The method name.</param>
        private void LogCommandEnd(int? recordsAffected, string name)
        {
            _messagePublisher.EmitStopMessage(
                new CommandDurationMessage(
                    _command.ConnectionId, 
                    _command.CommandId, 
                    recordsAffected).AsTimelineMessage("Command: Executed", AdomdTimelineCategory.Command, name));
        }

        /// <summary>
        /// Handles command error execution.
        /// Publishes a CommandErrorMessage.
        /// </summary>
        /// <param name="exception">The exception raised.</param>
        /// <param name="name">The method name.</param>
        private void LogCommandError(Exception exception, string name)
        {
            _messagePublisher.EmitStopMessage(
                new CommandErrorMessage(
                    _command.ConnectionId, 
                    _command.CommandId, 
                    exception).AsTimelineMessage("Command: Error", AdomdTimelineCategory.Command, name));
        }
    }
}
