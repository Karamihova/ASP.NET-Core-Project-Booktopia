namespace Booktopia.Models.Authors
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Author;
    public class BecomeAuthorFormModel
    {
        [Required(ErrorMessage = "Name of author is required.")]
        [StringLength(MaxName, MinimumLength = MinName, ErrorMessage = "Name should be between {2} and {1} characters long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(MaxDescription, MinimumLength = MinDescription, ErrorMessage = "Description should be between {2} and {1} characters long.")]
        public string Description { get; set; }
    }
}
