namespace Project2IdentityEmail.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool IsExist { get; set; } // Kullanıcıda bu rol var mı?
    }
}
