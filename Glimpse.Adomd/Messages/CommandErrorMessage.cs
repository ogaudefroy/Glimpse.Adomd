namespace Glimpse.Adomd.Messages
{
    using System;
    using Core.Message;

    /// <summary>
    /// Message published when a command error occurs.
    /// </summary>
    public class CommandErrorMessage : AdomdCommandMessage, ITimelineMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandErrorMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="exception">The exception thrown.</param>
        public CommandErrorMessage(Guid connectionId, Guid commandId, Exception exception)
            : this(connectionId, commandId, exception, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandErrorMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="exception">The exception thrown.</param>
        /// <param name="isAsync">Indicates whether or not the call is asynchronous.</param>
        public CommandErrorMessage(Guid connectionId, Guid commandId, Exception exception, bool isAsync)
            : base(connectionId, commandId)
        {
            Exception = exception;
            IsAsync = isAsync;
        }

        /// <summary>
        /// Gets the exception thrown.
        /// </summary>
        public Exception Exception { get; protected set; }

        /// <summary>
        /// Gets the event name.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets the event category.
        /// </summary>
        public TimelineCategoryItem EventCategory { get; set; }

        /// <summary>
        /// Gets the event sub text.
        /// </summary>
        public string EventSubText { get; set; }

        /// <summary>
        /// Gets a value indicating whether the call is asynchronous.
        /// </summary>
        public bool IsAsync { get; set; }
    }
}
