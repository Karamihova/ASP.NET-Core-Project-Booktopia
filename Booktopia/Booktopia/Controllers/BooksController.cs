namespace Booktopia.Controllers
{
    using Booktopia.Models.Books;
    using Microsoft.AspNetCore.Mvc;
    public class BooksController : Controller
    {
        public IActionResult All() => View();

        public IActionResult Write() => View();

        //[HttpPost]
        //public IActionResult Write(WriteBookFormModel book)
        //{

        //}
    }
}
