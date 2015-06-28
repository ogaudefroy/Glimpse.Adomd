namespace Glimpse.Adomd.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Metadata available for ADOMD connections.
    /// </summary>
    public class ConnectionMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionMetadata"/> class.
        /// </summary>
        /// <param name="id">The connection identifier.</param>
        public ConnectionMetadata(string id)
        {
            Id = id;
            Commands = new Dictionary<string, CommandMetadata>();
            Transactions = new Dictionary<string, TransactionMetadata>();
        }

        /// <summary>
        /// Gets the connection identifier.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the connection start time.
        /// </summary>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Gets the connection start count.
        /// </summary>
        public int StartCount { get; private set; }

        /// <summary>
        /// Gets the connection end time.
        /// </summary>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Gets the connection end count.
        /// </summary>
        public int EndCount { get; private set; }

        /// <summary>
        /// Gets a dictionary containing the commands associated.
        /// </summary>
        public IDictionary<string, CommandMetadata> Commands { get; private set; }

        /// <summary>
        /// Gets a dictionary containg the transactions associated.
        /// </summary>
        public IDictionary<string, TransactionMetadata> Transactions { get; private set; }

        /// <summary>
        /// Gets the connection open duration.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets the connection offset.
        /// </summary>
        public TimeSpan? Offset { get; set; }

        /// <summary>
        /// Registers an Open event on the connection.
        /// </summary>
        public void RegisterStart()
        {
            StartCount++;
        }

        /// <summary>
        /// Registers a Close event on the connection.
        /// </summary>
        public void RegisterEnd()
        {
            EndCount++;
        }

        /// <summary>
        /// Registers a command.
        /// </summary>
        /// <param name="command">The command itself.</param>
        public void RegisterCommand(CommandMetadata command)
        {
            Commands.Add(command.Id, command);
        }

        /// <summary>
        /// Registers a transaction start event that occured within the connection.
        /// </summary>
        /// <param name="transaction">The transaction itself.</param>
        public void RegisterTransactionStart(TransactionMetadata transaction)
        {
            Transactions.Add(transaction.Id, transaction);

            var command = Commands.FirstOrDefault(x => x.Value.Offset >= transaction.Offset);
            if (command.Value != null)
            {
                command.Value.HeadTransaction = transaction;
            }
        }

        /// <summary>
        /// Registers a transaction stop event that occured within the connection.
        /// </summary>
        /// <param name="transaction">The transaction itself.</param>
        public void RegisterTransactionEnd(TransactionMetadata transaction)
        {
            var command = Commands.LastOrDefault(x => x.Value.Offset <= transaction.Offset + transaction.Duration);
            if (command.Value != null)
            {
                command.Value.TailTransaction = transaction;
            }
        }
    }
}
