namespace Booktopia.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Review;
    public class Review
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(MaxText)]
        public string Text { get; set; }

        [Required]
        [Range(MinRating, MaxRating)]
        public int Rating { get; set; }

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
