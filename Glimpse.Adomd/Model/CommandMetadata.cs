namespace Glimpse.Adomd.Model
{
    using System;
    using System.Collections.Generic;

    public class CommandMetadata
    {
        public CommandMetadata(string id, string connectionId)
        {
            Id = id;
            ConnectionId = connectionId;
            Parameters = new List<CommandParameterMetadata>(); 
        }

        public string Id { get; private set; }

        public string ConnectionId { get; private set; }

        public string Command { get; set; }

        public Exception Exception { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public long? RecordsAffected { get; set; }

        public long? TotalRecords { get; set; }

        public TimeSpan Duration { get; set; }

        public TimeSpan Offset { get; set; }

        public IList<CommandParameterMetadata> Parameters { get; private set; }
        
        public int ExecutionIndex { get; set; }

        public bool HasTransaction { get; set; }
        
        public TransactionMetadata HeadTransaction { get; set; }

        public TransactionMetadata TailTransaction { get; set; }
    }
}
