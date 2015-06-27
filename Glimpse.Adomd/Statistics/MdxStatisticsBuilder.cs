namespace Glimpse.Adomd.Statistics
{
    using System;
    using System.Linq;
    using Model;

    /// <summary>
    /// Component in charge of building a MdxStatistics model from a QueryMetadata/
    /// </summary>
    internal static class MdxStatisticsBuilder
    {
        /// <summary>
        /// Builds a MdxStatistics from a <see cref="QueryMetadata"/> instance.
        /// </summary>
        /// <param name="queryMetadata">The source QueryMetadata.</param>
        /// <returns>A MdxStatistics instance.</returns>
        public static MdxStatistics Build(QueryMetadata queryMetadata)
        {
            var queryCount = queryMetadata.Commands.Count;
            var connectionCount = queryMetadata.Connections.Count;
            var transactionCount = queryMetadata.Transactions.Count;

            var queryExecutionTime = new TimeSpan();
            var connectionOpenTime = new TimeSpan();

            queryExecutionTime = queryMetadata.Commands.Aggregate(queryExecutionTime, (totalDuration, command) => totalDuration + command.Value.Duration);
            connectionOpenTime = queryMetadata.Connections.Aggregate(connectionOpenTime, (totalDuration, connection) => totalDuration + connection.Value.Duration.GetValueOrDefault(TimeSpan.Zero));

            return new MdxStatistics(
                queryCount,
                connectionCount,
                transactionCount,
                queryExecutionTime,
                connectionOpenTime);
        }
    }
}
