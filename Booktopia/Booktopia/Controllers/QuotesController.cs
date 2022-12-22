using Booktopia.Models.Quotes;
using Booktopia.Services.Books;
using Booktopia.Services.Quotes;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Booktopia.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IQuoteService quoteService;
        private readonly IBookService bookService;
        public QuotesController(IQuoteService quoteService, IBookService bookService)
        {
            this.quoteService = quoteService;
            this.bookService = bookService;
        }

        public IActionResult All()
        {
            var quotes = this.quoteService.All();
            return View(quotes);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(QuoteFormModel quote)
        {
            if (!ModelState.IsValid)
            {
                return View(quote);
            }


            var bookId = this.bookService.FindByTitle(quote.BookTitle);
            if(bookId == -1)
            {
                return BadRequest();
            }

            var quoteId = this.quoteService.Create(quote.Text, quote.BookTitle);

            if(quoteId == -1)
            {
                return BadRequest("There is no book with this title.");
            }

            return RedirectToAction("All", "Quotes");
        }
    }
}
