using System.Collections.Generic;
using System.Text;
using Microsoft.AnalysisServices.AdomdClient;

namespace Glimpse.Adomd.Sample.Models
{
    public class CubeBuilder
    {
        public static List<CubeViewModel> BuildViewModel(CellSet cellSet)
        {
            var model =
                new List<CubeViewModel>
                    {
                        new CubeViewModel
                            {
                                Column = BuildColums(cellSet),
                                Analytics = BuildAnalyticsRow(cellSet)
                            }
                    };
            return model;
        }

        private static string BuildAnalyticsRow(CellSet cellSet)
        {
            var result = new StringBuilder();
            if (cellSet.Axes.Count > 0)
            {
                var tuplesOnRows = cellSet.Axes[0].Set.Tuples;
                foreach (var row in tuplesOnRows)
                {
                    foreach (var t in row.Members)
                    {
                        result.Append(t.Caption + "\n");
                    }
                }
            }
            return result.ToString();
        }

        private static Column[] BuildColums(CellSet cellSet)
        {
            var result = new List<Column>();
            var tuplesOnColumns = cellSet.Axes[0].Set.Tuples;
            var i = 0;
            foreach (var column in tuplesOnColumns)
            {
                result.Add(new Column
                {
                    HeaderName = column.Members[0].Caption,
                    FormattedValue = cellSet.Cells[i].FormattedValue
                });
                i++;
            }
            return result.ToArray();
        }
    }
}