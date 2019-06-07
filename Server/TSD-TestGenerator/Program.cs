using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using TSDTestGenerator.Database;

namespace TSD_TestGenerator
{
    public class Program
    {
        public static QuestionsDbContext QuestionsDbContext;

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
