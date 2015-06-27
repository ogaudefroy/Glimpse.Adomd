namespace Glimpse.Adomd.Statistics
{
    using System;

    /// <summary>
    /// Data Model for MDX statistics.
    /// </summary>
    internal class MdxStatistics
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdxStatistics"/> class.
        /// </summary>
        /// <param name="queryCount">The number of queries.</param>
        /// <param name="connectionCount">The number of connections.</param>
        /// <param name="transactionCount">The number of transactions.</param>
        /// <param name="queryExecutionTime">The query execution duration.</param>
        /// <param name="connectionOpenTime">The connection open duration.</param>
        public MdxStatistics(int queryCount, int connectionCount, int transactionCount, TimeSpan queryExecutionTime, TimeSpan connectionOpenTime)
        {
            QueryCount = queryCount;
            ConnectionCount = connectionCount;
            TransactionCount = transactionCount;
            QueryExecutionTime = queryExecutionTime;
            ConnectionOpenTime = connectionOpenTime;
        }

        /// <summary>
        /// Gets the number of queries.
        /// </summary>
        public int QueryCount { get; private set; }

        /// <summary>
        /// Gets the number of active connections.
        /// </summary>
        public int ConnectionCount { get; private set; }

        /// <summary>
        /// Gets the number of transactions.
        /// </summary>
        public int TransactionCount { get; private set; }

        /// <summary>
        /// Gets the query execution duration.
        /// </summary>
        public TimeSpan QueryExecutionTime { get; private set; }

        /// <summary>
        /// Gets the connection open duration.
        /// </summary>
        public TimeSpan ConnectionOpenTime { get; private set; }
    }
}
