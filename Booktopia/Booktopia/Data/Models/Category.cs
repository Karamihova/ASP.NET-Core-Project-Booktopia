namespace Booktopia.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.Category;
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required, StringLength(MaxType)]
        public string Type { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
