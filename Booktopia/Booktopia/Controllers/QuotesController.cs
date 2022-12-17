using Booktopia.Models.Quotes;
using Booktopia.Services.Quotes;
using Microsoft.AspNetCore.Mvc;

namespace Booktopia.Controllers
{
    public class QuotesController : Controller
    {
        private readonly IQuoteService quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            this.quoteService = quoteService;
        }

        public IActionResult All()
        {
            return View();
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

            var quoteId = this.quoteService.Create(quote.Text, quote.BookTitle);

            if(quoteId == -1)
            {
                return BadRequest("There is no book with this title");
            }

            return RedirectToAction("All", "Quotes");
        }
    }
}
