using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AuthServer.Models
{
    public class ApplicationDbcontext: DbContext
    {
        public ApplicationDbcontext(DbContextOptions opts) : base(opts)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        //remove seeding
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var builder = modelBuilder.Entity<User>();

            // Adding the code below tells DB "NumericId is an AlternateKey and don't update".
            builder.Property(p => p.Id)
                .UseSqlServerIdentityColumn();
            builder.Property(p => p.Id)
                .Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            //seeding data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 3,
                    Username = "will",
                    Password = "1234",
                    UserType = "WebUser"
                }
            );

        }
    }
}
