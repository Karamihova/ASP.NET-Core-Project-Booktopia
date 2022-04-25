namespace Booktopia.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Quote;
    public class Quote
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(MaxText)]
        public string Text { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
