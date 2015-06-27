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

            var viewModel = CubeBuilder.BuildViewModel(secondRequest);

            return View(viewModel);
        }
    }
}