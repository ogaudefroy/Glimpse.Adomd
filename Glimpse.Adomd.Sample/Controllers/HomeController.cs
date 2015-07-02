using System.Web.Mvc;
using Glimpse.Adomd.Sample.Models;
using Glimpse.Adomd.Sample.Repositories;

namespace Glimpse.Adomd.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdventureWorksRepository repository;
        
        public HomeController()
        {
            repository = new MdxAdventureWorksRepository();
        }

        // GET: Home
        public ActionResult Index()
        {
            var firstRequest = repository.GetResultForFirstYear();
            var secondRequest = repository.GetResultForYear(2012);
            var thirdRequest = repository.GetInternetSalesAmount();
            var fourthRequest = repository.TestExecuteReader();
            var fifthRequest = repository.TestExecuteXmlReader();
            var dmvQuery = repository.TestDmv();

            var viewModel = CubeBuilder.BuildViewModel(thirdRequest);

            return View(viewModel);
        }
    }
}