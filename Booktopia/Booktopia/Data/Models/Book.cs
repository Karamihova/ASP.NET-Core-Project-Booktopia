namespace Booktopia.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    using static DataConstants.Book;
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }


        [Required,StringLength(AnnotationMaxLength)]
        public string Annotation { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Chapter> Chapters { get; set; } = new HashSet<Chapter>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<Quote> Quotes { get; set; } = new HashSet<Quote>();

    }
}
