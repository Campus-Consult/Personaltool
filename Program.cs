using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Personaltool.Data;

namespace Personaltool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                // auto migration only on release
                // context.Database.Migrate();

                CCDefaultDataSeed.DoSeed(context).Wait();

                var config = services.GetRequiredService<IConfiguration>();
                var randomTestSeedConfig = config.GetSection("RandomTestSeedConfig").Get<RandomTestSeedConfig>();
                RandomTestDataSeed.DoSeed(context, randomTestSeedConfig).Wait();
            }
            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
