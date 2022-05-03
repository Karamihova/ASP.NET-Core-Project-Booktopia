namespace Booktopia.Models.Chapters
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    using static Data.DataConstants.Chapter;
    public class ChapterFormModel
    {
        [Required(ErrorMessage = "Title of chapter is required.")]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = "Title should be between {2} and {1} characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Text of chapter is required.")]
        [StringLength(MaxText, MinimumLength = MinText, ErrorMessage = "Text should be between {2} and {1} characters long.")]
        public string Text { get; set; }

        public int BookId { get; set; }
    }
}
