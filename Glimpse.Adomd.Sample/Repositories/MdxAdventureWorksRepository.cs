namespace Glimpse.Adomd.Sample.Repositories
{
    using System.Configuration;
    using System.Xml;
    using System.Data;
    using AlternateType;
    using Microsoft.AnalysisServices.AdomdClient;

    public class MdxAdventureWorksRepository : IAdventureWorksRepository
    {
        private readonly string connStr;

        public MdxAdventureWorksRepository()
        {
            connStr = ConfigurationManager.ConnectionStrings["AdventureWorksDW2014"].ConnectionString;
        }
 
        public CellSet GetResultForYear(int year)
        {
            using (var conn = new GlimpseAdomdConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT [Date].[Calendar Year].[StrToMember(@CalendarYear)] on 0 FROM [Adventure Works];";
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = "CalendarYear";
                parameter.Value = "CY 2012";
                cmd.Parameters.Add(parameter);
                return cmd.ExecuteCellSet();
            }
        }

        public CellSet GetResultForFirstYear()
        {
            using (var conn = new GlimpseAdomdConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT [Date].[Calendar Year].FirstChild on 0 FROM [Adventure Works];";
                return cmd.ExecuteCellSet();
            }
        }

        public CellSet GetInternetSalesAmount()
        {
            using (var conn = new GlimpseAdomdConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"WITH SET [EUROPE] AS '{[Customer].[Country].&[France],
[Customer].[Country].&[Germany],
[Customer].[Country].&[United Kingdom]}' 
SELECT Measures.[Internet Sales Amount] ON COLUMNS,
[EUROPE] ON ROWS
FROM [Adventure Works];";
                return cmd.ExecuteCellSet();
            }
        }

        public DataTable TestExecuteReader()
        {
            using (var conn = new GlimpseAdomdConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT {[Measures].[Sales Amount],
[Measures].[Gross Profit Margin]} ON COLUMNS, 
{[Product].[Product Model Categories].[Category]} ON ROWS 
FROM [Adventure Works]
WHERE ([Sales Territory Country].[United States]);";
                DataSet ds = new DataSet {EnforceConstraints = false};
                ds.Tables.Add();
                DataTable dt = ds.Tables[0];
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }

        public XmlReader TestExecuteXmlReader()
        {
            using (var conn = new GlimpseAdomdConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"SELECT {[Measures].[Sales Amount],
[Measures].[Gross Profit Margin]} ON COLUMNS, 
{[Product].[Product Model Categories].[Category]} ON ROWS 
FROM [Adventure Works]
WHERE ([Sales Territory Country].[United States]);";
                return cmd.ExecuteXmlReader();
            }
        }


        public DataTable TestDmv()
        {
            using (var conn = new GlimpseAdomdConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "Select * from $System.discover_object_memory_usage";
                DataSet ds = new DataSet { EnforceConstraints = false };
                ds.Tables.Add();
                DataTable dt = ds.Tables[0];
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
    }
}