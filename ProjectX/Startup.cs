
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectX.Business.Attachment;
using ProjectX.Business.Caching;
using ProjectX.Business.Zone;
using ProjectX.Business.Product;
using ProjectX.Business.Email;
using ProjectX.Business.General;
using ProjectX.Business.Jwt;
using ProjectX.Business.Notifications;
using ProjectX.Business.Profile;
using ProjectX.Business.User;
using ProjectX.Business.BenefitTitle;
using ProjectX.Entities.AppSettings;
using ProjectX.Repository.AttachmentRepository;
using ProjectX.Repository.ZoneRepository;
using ProjectX.Repository.ProductRepository;
using ProjectX.Repository.EmailRepository;
using ProjectX.Repository.GeneralRepository;
using ProjectX.Repository.NotificationsRepository;
using ProjectX.Repository.ProfileRepository;
using ProjectX.Repository.UserRepository;
using ProjectX.Repository.CurrencyRateRepository;
using ProjectX.Repository.BenefitTitleRepository;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectX.Extension.Jwt;
using ProjectX.Extension.Excption;
using ProjectX.Business.Benefit;
using ProjectX.Repository.BenefitRepository;
using ProjectX.Business.Package;
using ProjectX.Repository.PackageRepository;
using ProjectX.Repository.TariffRepository;
using ProjectX.Business.Tariff;
using ProjectX.Business.CurrencyRate;
using ProjectX.Repository.PlanRepository;
using ProjectX.Business.Plan;
using ProjectX.Repository.ProductionRepository;
using ProjectX.Business.Production;
using ProjectX.Business.Beneficiary;
using ProjectX.Repository.BeneficiaryRepository;
using ProjectX.Business.Users;
using ProjectX.Repository.UsersRepository;


public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        TrAppSettings appSettings = _configuration.GetSection("AppSettings").Get<TrAppSettings>();

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.jwt.Key)),
                    RequireSignedTokens = true,
                    RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
                    ValidateLifetime = true, // <- the "exp" will be validated
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => false;
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
        });


        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

        services.AddAuthorization();


        //services.AddControllers();
        services.AddControllersWithViews().AddRazorRuntimeCompilation();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IDatabaseCaching, DatabaseCaching>();
        services.AddSingleton<IJwtBusiness, JwtBusiness>();


        services.AddSingleton<IBenefitBusiness, BenefitBusiness>();
        services.AddSingleton<IBenefitRepository, BenefitRepository>();

        services.AddSingleton<IPackageBusiness, PackageBusiness>();
        services.AddSingleton<IPackageRepository, PackageRepository>();

        services.AddSingleton<IProductBusiness, ProductBusiness>();
        services.AddSingleton<IProductRepository, ProductRepository>();



        services.AddSingleton<IProductionBusiness, ProductionBusiness>();
        services.AddSingleton<IProductionRepository, ProductionRepository>();

        services.AddSingleton<ITariffBusiness, TariffBusiness>();
        services.AddSingleton<ITariffRepository, TariffRepository>();

        services.AddSingleton<IZoneBusiness, ZoneBusiness>();
        services.AddSingleton<IZoneRepository, ZoneRepository>();

        services.AddSingleton<IGeneralBusiness, GeneralBusiness>();
        services.AddSingleton<IGeneralRepository, GeneralRepository>();

        services.AddSingleton<IUsersBusiness, UsersBusiness>();
        services.AddSingleton<IUsersRepository, UsersRepository>();

        services.AddSingleton<IUserBusiness, UserBusiness>();
        services.AddSingleton<IUserRepository, UserRepository>();

        services.AddSingleton<IPlanRepository, PlanRepository>();
        services.AddSingleton<IPlanBusiness, PlanBusiness>();

        services.AddSingleton<IProfileBusiness, ProfileBusiness>();
        services.AddSingleton<IProfileRepository, ProfileRepository>();

        services.AddSingleton<IAttachmentBusiness, AttachmentBusiness>();
        services.AddSingleton<IAttachmentRepository, AttachmentRepository>();

        services.AddSingleton<IEmailBusiness, EmailBusiness>();
        services.AddSingleton<IEmailRepository, EmailRepository>();

        services.AddSingleton<INotificationsBusiness, NotificationsBusiness>();
        services.AddSingleton<INotificationsRepository, NotificationsRepository>();

        services.AddSingleton<IBeneficiaryBusiness, BeneficiaryBusiness>();
        services.AddSingleton<IBeneficiaryRepository, BeneficiaryRepository>();

        services.AddSingleton<ICurrencyRateBusiness, CurrencyRateBusiness>();
        services.AddSingleton<ICurrencyRateRepository, CurrencyRateRepository>();

        services.AddSingleton<IBenTitleBusiness, BenTitleBusiness>();
        services.AddSingleton<IBenTitleRepository, BenTitleRepository>();
        
        //services.AddSingleton<IHelperNONsql, HelperNONsql>();

        services.AddMvc().AddControllersAsServices();


        services.AddDistributedMemoryCache();
        services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseCors(x => x.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

        app.UseAuthentication();
        app.UseAuthorization();

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
        {
            app.UseExceptionHandler("/sa/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();


        app.UseJwtMiddleware();
        app.UseExcptionMiddleware();



        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=login}/{action=Index}/{id?}");
        });

    }
}
