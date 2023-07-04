using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.Tariff;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.dbModels;

namespace ProjectX.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TrAppSettings _appSettings;
        private User _user;

        public ErrorController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor )
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
        }


        public IActionResult Index()
        {
            return View("Error");
        }
    }
}
