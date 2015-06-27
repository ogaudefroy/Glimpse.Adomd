namespace Glimpse.Adomd.Messages
{
    using Core.Message;

    /// <summary>
    /// Timeline configuration for category Adomd.
    /// </summary>
    public class AdomdTimelineCategory : TimelineCategory
    {
        private static readonly TimelineCategoryItem connection = new TimelineCategoryItem("Connection", "#F0ED5D", "#DEE81A");
        private static readonly TimelineCategoryItem command = new TimelineCategoryItem("Command", "#FD45F7", "#DD31DA");

        /// <summary>
        /// Gets the timeline category for a connection.
        /// </summary>
        public static TimelineCategoryItem Connection
        {
            get { return connection; }
        }

        /// <summary>
        /// Gets the timeline category for a command.
        /// </summary>
        public static TimelineCategoryItem Command
        {
            get { return command; }
        }
    }
}
