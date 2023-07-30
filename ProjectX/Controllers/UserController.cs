using ProjectX.Business.Jwt;
using ProjectX.Entities.AppSettings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using ProjectX.Entities;
using ProjectX.Entities.dbModels;

namespace ProjectX.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IJwtBusiness _jwtBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        public UserController(IHttpContextAccessor httpContextAccessor, IJwtBusiness jwtBusiness, IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _jwtBusiness = jwtBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public CookieUser ValidateUser()
        //{            
        //    try
        //    {
        //        string token = _httpContextAccessor.HttpContext.Request.Headers["token"].ToString();
        //        return _jwtBusiness.getUserFromToken(token, _appSettings.jwt);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}