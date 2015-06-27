namespace Glimpse.Adomd.Messages
{
    using System;
    using Core.Message;

    /// <summary>
    /// Message published once connection has been closed.
    /// </summary>
    public class ConnectionClosedMessage : AdomdMessage, ITimelineMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionClosedMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        public ConnectionClosedMessage(Guid connectionId)
            : base(connectionId)
        {
        }

        /// <summary>
        /// Gets or sets the event name.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the event category.
        /// </summary>
        public TimelineCategoryItem EventCategory { get; set; }

        /// <summary>
        /// Gets or sets the event text.
        /// </summary>
        public string EventSubText { get; set; }
    }
}
