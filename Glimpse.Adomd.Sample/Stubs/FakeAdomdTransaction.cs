namespace Glimpse.Adomd.Sample.Stubs
{
    using System.Data;

    public class FakeAdomdTransaction : IDbTransaction
    {
        public FakeAdomdTransaction(FakeAdomdConnection connection)
        {
            this.Connection = connection;
        }

        public void Commit()
        {
        }

        public IDbConnection Connection
        {
            get;
            private set;
        }

        public IsolationLevel IsolationLevel
        {
            get { return IsolationLevel.ReadCommitted; }
        }

        public void Rollback()
        {
        }

        public void Dispose()
        {
        }
    }
}