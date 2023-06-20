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
using ProjectX.Entities.AppSettings;
using ProjectX.Repository.AttachmentRepository;
using ProjectX.Repository.ZoneRepository;
using ProjectX.Repository.ProductRepository;
using ProjectX.Repository.EmailRepository;
using ProjectX.Repository.GeneralRepository;
using ProjectX.Repository.NotificationsRepository;
using ProjectX.Repository.ProfileRepository;
using ProjectX.Repository.UserRepository;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProjectX.Extension.Jwt;
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

        services.AddSingleton<IProductBusiness, ProductBusiness>();
        services.AddSingleton<IProductRepository, ProductRepository>();

        services.AddSingleton<IZoneBusiness, ZoneBusiness>();
        services.AddSingleton<IZoneRepository, ZoneRepository>();

        services.AddSingleton<IGeneralBusiness, GeneralBusiness>();
        services.AddSingleton<IGeneralRepository, GeneralRepository>();

        services.AddSingleton<IUserBusiness, UserBusiness>();
        services.AddSingleton<IUserRepository, UserRepository>();

        services.AddSingleton<IProfileBusiness, ProfileBusiness>();
        services.AddSingleton<IProfileRepository, ProfileRepository>();

        services.AddSingleton<IAttachmentBusiness, AttachmentBusiness>();
        services.AddSingleton<IAttachmentRepository, AttachmentRepository>();

        services.AddSingleton<IEmailBusiness, EmailBusiness>();
        services.AddSingleton<IEmailRepository, EmailRepository>();

        services.AddSingleton<INotificationsBusiness, NotificationsBusiness>();
        services.AddSingleton<INotificationsRepository, NotificationsRepository>();

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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=login}/{action=Index}/{id?}");
        });

    }
}
