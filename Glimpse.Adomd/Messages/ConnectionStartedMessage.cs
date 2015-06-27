namespace Glimpse.Adomd.Messages
{
    using System;

    /// <summary>
    /// Message published juste after the connection has been opened.
    /// </summary>
    public class ConnectionStartedMessage : AdomdMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStartedMessage"/> class.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        public ConnectionStartedMessage(Guid connectionId)
            : base(connectionId)
        {
        }
    }
}
