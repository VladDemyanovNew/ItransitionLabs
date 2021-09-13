using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task4.Data;

[assembly: HostingStartup(typeof(Task4.Areas.Identity.IdentityHostingStartup))]
namespace Task4.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Task4Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Task4ContextConnection")));

                /*services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Task4Context>();*/
            });
        }
    }
}