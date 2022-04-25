namespace Booktopia.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    using static DataConstants.Chapter;
    public class Chapter
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required,StringLength(MaxText)]  
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
