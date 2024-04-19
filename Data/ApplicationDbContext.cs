using Key_Management_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Key_Management_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public DbSet<LogoutToken> LogoutTokens { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<KeyCollector> KeyCollectors { get; set; }
        public DbSet<Key> Key { get; set; }
        public DbSet<RequestKey> RequestKey { get; set; }
        public DbSet<ThirdParty> ThirdParty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
