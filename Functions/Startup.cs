using System;
using DataLayer.Data;
using DataLayer.Models;
using Functions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace Functions
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options1 =>
            {
                options1.UseSqlServer(
                    Config.Get("DefaultConnection"),
                    builder =>
                    {
                        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                        builder.CommandTimeout(10);
                    }
                );
            });

            builder.Services.AddHttpClient();
        }
    }
}
