namespace Glimpse.Adomd.Messages
{
    using System;
    using Core.Message;

    /// <summary>
    /// Base class for Adomdmessages.
    /// </summary>
    public abstract class AdomdMessage : ITimedMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdomdMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        protected AdomdMessage(Guid connectionId)
        {
            Id = Guid.NewGuid();
            ConnectionId = connectionId;
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the message duration.
        /// </summary>
        public TimeSpan Duration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        public Guid Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the connection identifier.
        /// </summary>
        public Guid ConnectionId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public TimeSpan Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the starting time.
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }
    }
}
