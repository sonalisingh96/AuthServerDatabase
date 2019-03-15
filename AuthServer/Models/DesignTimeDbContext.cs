using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{

    //TBD not needed
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<ApplicationDbcontext>
    {
        public ApplicationDbcontext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbcontext>();
          //  var connectionString = configuration.GetConnectionString("Data Source=(localdb)\\MSSQLLocalDB;Database=IdentityServerDatabase;Trusted_Connection=True");
            builder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=IdentityServerDatabase;Trusted_Connection=True");
            return new ApplicationDbcontext(builder.Options);
        }
    }
}
