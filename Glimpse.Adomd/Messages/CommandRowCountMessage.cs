using System;

namespace Glimpse.Adomd.Messages
{
    public class CommandRowCountMessage : AdomdCommandMessage
    {
        public CommandRowCountMessage(Guid connectionId, Guid commandId, long rowCount)
            : base(connectionId, commandId)
        {
            RowCount = rowCount;
        }

        public long RowCount { get; protected set; }
    }
}
