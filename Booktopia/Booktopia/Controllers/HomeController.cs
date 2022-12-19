using Booktopia.Models;
using Booktopia.Services.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Booktopia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            this.homeService = homeService;
        }

        public IActionResult Index()
        {
            var infoFromDatabase = homeService.GetInfoFromDatabase();
            return View(infoFromDatabase);
        }

        public IActionResult Error() => View();
    }
}
