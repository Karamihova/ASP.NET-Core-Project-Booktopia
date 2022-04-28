namespace Booktopia.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.User;
    public class User : IdentityUser
    {
        [MaxLength(MaxFullName)]
        public string FullName { get; set; }
    }
}
