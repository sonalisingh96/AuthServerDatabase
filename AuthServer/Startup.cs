using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AuthServer.Models;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace AuthServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddMvc();

            services.AddDbContext<ApplicationDbcontext>(opts =>
                    opts.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=IdentityServerDatabase;Trusted_Connection=True"));
           // services.AddDbContext<ApplicationDbcontext>(opts => opts.UseInMemoryDatabase("test"));
            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>()
                .AddTransient<UserRepository>();
   // .AddTransient<IAuthRepository, AuthRepository>();
            services.AddIdentityServer()
                .AddSigningCredential(new X509Certificate2("C:\\workspace\\IdentityServer4Auth.pfx", "ABC$1234"))

               //.AddInMemoryIdentityResources(Config.GetIdentityResources())
               //  .AddTestUsers(Config.GetUsers())
               .AddInMemoryApiResources(Config.GetApiResources())
               .AddInMemoryClients(Config.GetClients())
               .AddProfileService<ProfileService>();
            services.AddScoped<UserRepository>();
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbcontext context)
        {
           // LoggerFactory.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        //    context.Database.EnsureCreated();
            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
