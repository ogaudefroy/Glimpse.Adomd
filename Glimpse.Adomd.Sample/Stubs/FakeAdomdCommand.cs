using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace Glimpse.Adomd.Sample.Stubs
{
    public class FakeAdomdCommand : IAdomdCommand
    {
        public FakeAdomdCommand()
        {
            this.Parameters = new FakeDataParameterCollection();
        }

        public string CommandText { get; set; }

        public int CommandTimeout { get; set; }

        public CommandType CommandType { get; set; }

        public IDbConnection Connection { get; set; }

        public IDataParameterCollection Parameters
        {
            get;
            private set;
        }

        public IDbTransaction Transaction { get; set; }

        public UpdateRowSource UpdatedRowSource { get; set; }

        public void Cancel()
        {
        }

        public IDbDataParameter CreateParameter()
        {
            return new AdomdParameter();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public CellSet ExecuteCellSet()
        {
            return null; //new CellSet();
        }

        public int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader()
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public object Execute()
        {
            throw new NotImplementedException();
        }

        public System.Xml.XmlReader ExecuteXmlReader()
        {
            throw new NotImplementedException();
        }
    }
}