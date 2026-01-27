using Microsoft.AspNetCore.Identity;

namespace Project2IdentityEmail.Entities
{
    public class AppUser : IdentityUser // IdentityUser sınıfını genişletiyoruz
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
    }
}
