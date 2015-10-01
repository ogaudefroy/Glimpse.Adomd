namespace Glimpse.Adomd.Messages
{
    using System;
    using Core.Message;

    /// <summary>
    /// Message published when a command finished its execution.
    /// </summary>
    public class CommandDurationMessage : AdomdCommandMessage, ITimelineMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDurationMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="recordsAffected">The number of records affected.</param>
        public CommandDurationMessage(Guid connectionId, Guid commandId, long? recordsAffected)
            : this(connectionId, commandId, recordsAffected, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandDurationMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        /// <param name="recordsAffected">The number of records affected.</param>
        /// <param name="isAsync">True if command has been executed asynchronously, False otherwise.</param>
        public CommandDurationMessage(Guid connectionId, Guid commandId, long? recordsAffected, bool isAsync)
            : base(connectionId, commandId)
        {
            CommandId = commandId;
            IsAsync = isAsync;
        }
        
        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the event category in the timeline.
        /// </summary>
        public TimelineCategoryItem EventCategory { get; set; }
        
        /// <summary>
        /// Gets or sets the event sub text.
        /// </summary>
        public string EventSubText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the command has been executed asynchronously.
        /// </summary>
        public bool IsAsync { get; set; }
    }
}
