namespace Project2IdentityEmail.DTOs
{
    public class LoginUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Beni Hatırla
        public bool IsPersistent { get; set; }
    }
}