using System.ComponentModel.DataAnnotations;

namespace Project2IdentityEmail.DTOs
{
    public class CreateUserRegisterDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Lütfen bir profil fotoğrafı seçiniz.")]
        public IFormFile ImageFile { get; set; } 
    }
}