namespace Booktopia.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Author;
    public class Author
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(MaxName)]
        public string Name { get; set; }

        [Required, StringLength(MaxDescription)]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public ICollection<Book> WrittenBooks { get; set; } = new HashSet<Book>();
    }
}
