using EDStationDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EDStationDatabase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [Route("home/entities/")]
        public IActionResult Entities()
        {
            var entities = new List<string> { "Station", "System", "Economy", "Allegiance", "Station Type" };
            return View(entities);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
