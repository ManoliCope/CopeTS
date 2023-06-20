using ProjectX.Entities.AppSettings;

public class Program
{
    public static void Main(string[] args)
    {
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
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}
