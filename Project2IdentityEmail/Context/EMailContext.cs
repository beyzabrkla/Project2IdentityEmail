using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Context
{
    // IdentityDbContext'e AppUser, AppRole ve ID tipi olan int'i tanıtıyoruz
    public class EMailContext : IdentityDbContext<AppUser, AppRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=BEYZA\\BEYZA_DEV; Initial Catalog=Project2IdentityDb; Integrated Security=True; Encrypt=False;");
        }

        // Tasarımdaki diğer tablolar (Mesajlar, Ayarlar vb.) ileride buraya DbSet olarak eklenecek
    }
}