using Microsoft.Extensions.Logging.Configuration;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

namespace MyBlog.App
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
        }

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.confing").GetCurrentClassLogger();
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
				}).UseNLog();

		}
	}
}