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

        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // UserMessage ile Category arasındaki ilişkiyi yapılandırma
            builder.Entity<UserMessage>()
                .HasOne(um => um.Category)
                .WithMany(c => c.Messages)
                .HasForeignKey(um => um.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            // Gönderen ilişkisi (Çift ilişki olduğu için OnDelete kısıtlaması şart)
            builder.Entity<UserMessage>()
                .HasOne(x => x.Sender)
                .WithMany(y => y.SendMessages)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Alıcı ilişkisi
            builder.Entity<UserMessage>()
                .HasOne(x => x.Receiver)
                .WithMany(y => y.ReceivedMessages)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}