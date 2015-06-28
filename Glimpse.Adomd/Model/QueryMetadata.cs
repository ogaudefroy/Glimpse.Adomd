namespace Glimpse.Adomd.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Metadata available for whole ADOMD.
    /// </summary>
    public class QueryMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryMetadata"/> class.
        /// </summary>
        public QueryMetadata()
        {
            Connections = new Dictionary<string, ConnectionMetadata>();
            Commands = new Dictionary<string, CommandMetadata>();
            Transactions = new Dictionary<string, TransactionMetadata>();
        }

        /// <summary>
        /// Gets a dictionary of ADOMD connections.
        /// </summary>
        public IDictionary<string, ConnectionMetadata> Connections { get; private set; }

        /// <summary>
        /// Gets a dictionary of ADOMD commands.
        /// </summary>
        public IDictionary<string, CommandMetadata> Commands { get; private set; }

        /// <summary>
        /// Gets a dictionary of ADOMD transactions.
        /// </summary>
        public IDictionary<string, TransactionMetadata> Transactions { get; private set; } 
    }
}
