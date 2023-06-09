using ProjectX.Entities.AppSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProjectX.Controllers
{
    public class NavigateController : Controller
    {
        private readonly CcAppSettings _appSettings;

        public NavigateController(IOptions<CcAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }


        public IActionResult Index(string pagename, string parameter)
        {
            var Host = _appSettings.webPagesHosting.Host + pagename.Trim() + ".aspx?" + parameter;
            ViewData["Host"] = Host;

            //ViewData["Pagename"] = pagename;
            //ViewData["Parameter"] = parameter;
            return PartialView("Navigate");
        }

        [HttpGet]
        public string geturl()
        {
            string Host = _appSettings.webPagesHosting.Host ;
            return Host;
        }
    }
}
