namespace Booktopia.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Review
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(ReviewMaxText)]
        public string Text { get; set; }

        [Required]
        [Range(ReviewMinRating, ReviewMaxRating)]
        public int Rating { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
