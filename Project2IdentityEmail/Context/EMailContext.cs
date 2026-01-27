using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Context
{
    public class EMailContext:IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BEYZA\\BEYZA_DEV; Initial Catalog=Project2IdentityDb; Integrated Security=True; Encrypt=False;");
        }
    }
}
