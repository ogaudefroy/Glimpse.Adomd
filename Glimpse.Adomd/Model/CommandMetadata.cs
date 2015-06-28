namespace Glimpse.Adomd.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Metadata available for an ADOMD command.
    /// </summary>
    public class CommandMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandMetadata"/> class.
        /// </summary>
        /// <param name="id">The command identifier.</param>
        /// <param name="connectionId">The connection identifier.</param>
        public CommandMetadata(string id, string connectionId)
        {
            Id = id;
            ConnectionId = connectionId;
            Parameters = new List<CommandParameterMetadata>(); 
        }

        /// <summary>
        /// Gets the commmand identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        public string ConnectionId { get; private set; }

        /// <summary>
        /// Gets or sets the command text.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the exception which occured during the command execution.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the command start time.
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the command end time.
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the number of records affected.
        /// </summary>
        public long? RecordsAffected { get; set; }

        /// <summary>
        /// Gets or sets the number of records.
        /// </summary>
        public long? TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the command duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the command offset.
        /// </summary>
        public TimeSpan Offset { get; set; }

        /// <summary>
        /// Gets the list of parameters associated with the command.
        /// </summary>
        public IList<CommandParameterMetadata> Parameters { get; private set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the command occured within a transaction.
        /// </summary>
        public bool HasTransaction { get; set; }

        /// <summary>
        /// Gets a reference on the transaction head metadata.
        /// </summary>
        public TransactionMetadata HeadTransaction { get; set; }

        /// <summary>
        /// Gets a reference on the transaction tail metadata.
        /// </summary>
        public TransactionMetadata TailTransaction { get; set; }
    }
}
