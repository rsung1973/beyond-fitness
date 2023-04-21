using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLib.Core.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CommonLib.DataAccess;
using WebHome.Models.DataEntity;
using WebHome;
using WebHome.Helper;

namespace TestConsole.Core
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var host = CreateHostBuilder(args).Build();
            //host.Run();
            Test01();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddProvider(new FileLoggerProvider());
                    logging.AddDebug();
                    //logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        static void Test01()
        {

            using (GenericManager<BFDataContext> models = new GenericManager<BFDataContext>())
            {
                var sampleItem = models.GetTable<MonthlyIndicator>().Where(i => i.Year == 2022 && i.Month == 8)
                        .FirstOrDefault();
                var price = sampleItem.CalculateAverageLessonPrice(models, 4869);
                Console.WriteLine(price);
            }
        }
    }
}
