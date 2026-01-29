using Microsoft.AspNetCore.Identity;

namespace Project2IdentityEmail.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public DateTime ConfirmDate { get; set; } = DateTime.Now;
    }
}