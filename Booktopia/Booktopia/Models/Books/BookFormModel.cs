﻿namespace Booktopia.Models.Books
{
    using Booktopia.Services.Books;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    using static Data.DataConstants.Book;
    public class BookFormModel
    {
        [Required(ErrorMessage = "Title of book is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title should be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Annotation of book is required.")]
        [StringLength(AnnotationMaxLength, MinimumLength = AnnotationMinLength, ErrorMessage = "Annotation should be between {2} and {1} characters long.")]
        public string Annotation { get; set; }

        [Display(Name = "Image URL")]
        [Required(ErrorMessage = "Image URL of is required.")]
        [Url(ErrorMessage = "Invalid URL.")]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category type is required.")]
        public int CategoryId { get; set; }

        public IEnumerable<BookCategoryServiceModel> Categories { get; set; }

    }
}
