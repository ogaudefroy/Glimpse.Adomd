namespace Glimpse.Adomd.Messages
{
    using System;

    /// <summary>
    /// Base class for command messages.
    /// </summary>
    public abstract class AdomdCommandMessage : AdomdMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdomdCommandMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        /// <param name="commandId">The command identifier.</param>
        protected AdomdCommandMessage(Guid connectionId, Guid commandId)
            : base(connectionId)
        {
            CommandId = commandId;
        }

        /// <summary>
        /// Gets the command identifier.
        /// </summary>
        public Guid CommandId { get; protected set; }
    }
}
