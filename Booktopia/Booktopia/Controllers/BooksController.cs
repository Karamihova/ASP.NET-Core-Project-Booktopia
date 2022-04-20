using Microsoft.AspNetCore.Mvc;

namespace Booktopia.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
