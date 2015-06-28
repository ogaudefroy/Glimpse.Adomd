using System.Data;
using System.Xml;
using Microsoft.AnalysisServices.AdomdClient;

namespace Glimpse.Adomd.Sample.Repositories
{
    interface IAdventureWorksRepository
    {
        CellSet GetResultForYear(int year);

        CellSet GetResultForFirstYear();

        CellSet GetInternetSalesAmount();

        DataTable TestExecuteReader();

        XmlReader TestExecuteXmlReader();
    }
}
