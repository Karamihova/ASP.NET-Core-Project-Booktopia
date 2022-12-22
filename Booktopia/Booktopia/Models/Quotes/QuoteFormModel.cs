namespace Booktopia.Models.Quotes
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    using static Data.DataConstants.Quote;
    public class QuoteFormModel
    {
        [Required(ErrorMessage = "Text of quote is required.")]
        [StringLength(MaxText, MinimumLength = MinText, ErrorMessage = "Quote should be between {2} and {1} characters long.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Book title of quote is required.")]
        public string BookTitle { get; set; }

        public int BookId { get; set; }
    }
}
