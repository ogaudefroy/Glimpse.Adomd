using System.Configuration;
using Glimpse.Adomd.AlternateType;
using Microsoft.AnalysisServices.AdomdClient;

namespace Glimpse.Adomd.Sample.Repositories
{
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
    }
}