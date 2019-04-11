using Microsoft.EntityFrameworkCore;

namespace AuthServer.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions opts) : base(opts)
        {
        }
        public DbSet<DbUser> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DbUser>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
