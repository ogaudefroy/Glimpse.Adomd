namespace Glimpse.Adomd.Messages
{
    /// <summary>
    /// Contract for publisher broadcasting timed messages.
    /// </summary>
    public interface ITimedMessagePublisher
    {
        /// <summary>
        /// Publishes a start message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        void EmitStartMessage(AdomdMessage message);

        /// <summary>
        /// Publishes a stop message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        void EmitStopMessage(AdomdMessage message);
    }
}
