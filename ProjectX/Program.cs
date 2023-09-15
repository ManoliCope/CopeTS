//using NLog.Web;
using ProjectX.Entities.AppSettings;

public class Program
{
    public static void Main(string[] args)
    {
        //var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddEnvironmentVariables();
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddCommandLine(args);
        })
        .ConfigureServices((hostContext, services) =>
        {
            //IConfiguration configuration = hostContext.Configuration;
            //services.AddSingleton(configuration);
            //services.Configure<TrAppSettings>(configuration.GetSection("MySection"));
            services.Configure<TrAppSettings>(hostContext.Configuration.GetSection("AppSettings"));

        })
        //.ConfigureLogging(logging =>
        //{
        //    logging.ClearProviders();
        //    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        //})
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            //webBuilder.UseNLog();
        });
}
