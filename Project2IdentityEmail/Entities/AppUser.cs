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
        public string? ConfirmCode { get; set; }
        public DateTime ConfirmDate { get; set; } = DateTime.Now;

        // Context'teki hatayı çözecek olan kısım:
        public virtual ICollection<UserMessage> SendMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }
    }
}