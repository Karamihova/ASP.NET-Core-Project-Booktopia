namespace Booktopia.Models.Books
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class WriteBookFormModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(AnnotationMinLength)]
        [MaxLength(AnnotationMaxLength)]
        public string Annotation { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public ICollection<BookCategoryViewModel> Categories { get; set; }

    }
}
