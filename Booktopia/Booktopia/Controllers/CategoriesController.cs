using Microsoft.AspNetCore.Mvc;

namespace Booktopia.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
