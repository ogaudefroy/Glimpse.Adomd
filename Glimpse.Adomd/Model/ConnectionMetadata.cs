using System;

namespace Glimpse.Adomd.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class ConnectionMetadata
    {
        public ConnectionMetadata(string id)
        {
            Id = id;
            Commands = new Dictionary<string, CommandMetadata>();
            Transactions = new Dictionary<string, TransactionMetadata>();
        }

        public string Id { get; private set; }

        public DateTime? StartDateTime { get; set; }

        public int StartCount { get; private set; }

        public DateTime? EndDateTime { get; set; }

        public int EndCount { get; private set; }

        public IDictionary<string, CommandMetadata> Commands { get; private set; }

        public IDictionary<string, TransactionMetadata> Transactions { get; private set; }

        public TimeSpan? Duration { get; set; }

        public TimeSpan? Offset { get; set; }

        public void RegisterStart()
        {
            StartCount++;
        }

        public void RegisterEnd()
        {
            EndCount++;
        }

        public void RegisterCommand(CommandMetadata command)
        {
            Commands.Add(command.Id, command);
        }

        public void RegisterTransactionStart(TransactionMetadata transaction)
        {
            Transactions.Add(transaction.Id, transaction);

            var command = Commands.FirstOrDefault(x => x.Value.Offset >= transaction.Offset);
            if (command.Value != null)
            {
                command.Value.HeadTransaction = transaction;
            }
        }

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
