using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Database
{

    
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<DbUser> Users { get; set; }
    }
}
