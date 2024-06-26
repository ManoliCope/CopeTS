﻿
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
using ProjectX.Business.BenefitTitle;
using ProjectX.Business.Report;
using ProjectX.Entities.AppSettings;
using ProjectX.Repository.AttachmentRepository;
using ProjectX.Repository.ZoneRepository;
using ProjectX.Repository.ProductRepository;
using ProjectX.Repository.EmailRepository;
using ProjectX.Repository.GeneralRepository;
using ProjectX.Repository.NotificationsRepository;
using ProjectX.Repository.ProfileRepository;
using ProjectX.Repository.CurrencyRateRepository;
using ProjectX.Repository.BenefitTitleRepository;
using ProjectX.Repository.ReportRepository;
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
using ProjectX.Interfaces;
using ProjectX.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using ProjectX.Interfaces;
using ProjectX.Services;
using Microsoft.Extensions.FileProviders;
using ProjectX.Repository.ProductionBatch;
using ProjectX.Repository.ProductionBatchRepository;
using ProjectX.Repository.ContextRepository;
using ProjectX.Business.PrepaidAccounts;
using ProjectX.Repository.PrepaidAccountsRepository;

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

        //services.addantiforgery(options =>
        //{
        //    options.suppressxframeoptionsheader = true;
        //    options.cookie.name = "customantiforgery"; 
        //});

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

        services.AddSingleton<IReportBusiness, ReportBusiness>();
        services.AddSingleton<IReportRepository, ReportRepository>();

        services.AddSingleton<IProductionBatchBusiness, ProductionBatchBusiness>();
        services.AddSingleton<IProductionBatchRepository, ProductionBatchRepository>();

        services.AddSingleton<IPrepaidAccountsBusiness, PrepaidAccountsBusiness>();
        services.AddSingleton<IPrepaidAccountsRepository, PrepaidAccountsRepository>();

        services.AddSingleton<TravelContext>();



        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        services.AddMvc().AddControllersAsServices();

        services.AddTransient<IDocumentService, DocumentService>();
        services.AddTransient<IRazorRendererHelper, RazorRendererHelper>();


        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });


        //services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseSession();

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

        TrAppSettings appSettings = _configuration.GetSection("AppSettings").Get<TrAppSettings>();
        var uploadsDirectory = appSettings.UploadUsProduct.UploadsDirectory;

        //app.UseStaticFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadsDirectory),
            RequestPath = "/" + appSettings.ExternalFolder.Staticpathname
        });


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=login}/{action=Index}/{id?}");
        });

    }
}
