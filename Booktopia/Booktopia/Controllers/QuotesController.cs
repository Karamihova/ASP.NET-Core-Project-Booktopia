using Microsoft.AspNetCore.Mvc;

namespace Booktopia.Controllers
{
    public class QuotesController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
