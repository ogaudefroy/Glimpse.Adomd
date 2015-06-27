using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;

namespace Glimpse.Adomd.Sample.Stubs
{
    public class FakeAdomdConnection : IAdomdConnection
    {
        public FakeAdomdConnection()
        {
            this.ConnectionId = Guid.NewGuid();
        }

        public Guid ConnectionId
        {
            get;
            private set;
        }
 
        public string ConnectionString
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public int ConnectionTimeout
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public CubeCollection Cubes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ConnectionState State
        {
            get
            {
                return ConnectionState.Open;
            }
        }

        public IDbTransaction BeginTransaction()
        {
            return new FakeAdomdTransaction(this);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return new FakeAdomdTransaction(this);
        }

        public void ChangeDatabase(string databaseName)
        {
        }

        public void Close()
        {
        }

        public IAdomdCommand CreateCommand()
        {
            return new FakeAdomdCommand();
        }

        public void Dispose()
        {
        }

        public void Open()
        {
        }

        IDbCommand IDbConnection.CreateCommand()
        {
            return new FakeAdomdCommand();
        }

        public MiningModelCollection MiningModels
        {
            get { throw new NotImplementedException(); }
        }

        public MiningStructureCollection MiningStructures
        {
            get { throw new NotImplementedException(); }
        }

        public MiningServiceCollection MiningServices
        {
            get { throw new NotImplementedException(); }
        }

        public string ProviderVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string ClientVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string ServerVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string SessionID
        {
            get { throw new NotImplementedException(); }
        }

        public bool ShowHiddenObjects
        {
            get { throw new NotImplementedException(); }
        }


        public void Close(bool endSession)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSchemaDataSet(Guid schema, object[] restrictions)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSchemaDataSet(Guid schema, object[] restrictions, bool throwOnInlineErrors)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSchemaDataSet(string schema, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions)
        {
            throw new NotImplementedException();
        }

        public DataSet GetSchemaDataSet(string schemaName, string schemaNamespace, AdomdRestrictionCollection restrictions, bool throwOnInlineErrors)
        {
            throw new NotImplementedException();
        }

        public void RefreshMetadata()
        {
            throw new NotImplementedException();
        }
    }
}