using ProjectX.Business.Jwt;
using ProjectX.Entities.AppSettings;
using ProjectX.Business.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using ProjectX.Entities.Models.User;
using ProjectX.Entities;

namespace ProjectX.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserBusiness _userBusiness;
        private IJwtBusiness _jwtBusiness;
        private readonly CcAppSettings _appSettings;

        public LoginController(IHttpContextAccessor httpContextAccessor, IJwtBusiness jwtBusiness, IOptions<CcAppSettings> appIdentitySettingsAccessor, IUserBusiness userBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _userBusiness = userBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _jwtBusiness = jwtBusiness;
        }
       
        public ActionResult Index(string cid)
        {
            //string avayaTempData = string.Concat(cid);
            //if (TempData.ContainsKey(avayaTempData))
            //{
            //    AvayaCallReq callinfo = JsonConvert.DeserializeObject<AvayaCallReq>(TempData[avayaTempData].ToString());
            //    TempData.Remove(avayaTempData);
            //}

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("token");
            return View();
        }

        [HttpPost]
        public LoginResp login(LoginReq req)
        {
            LoginResp response = _userBusiness.Login(req);

            if (response.user != null)
            {
                CookieUser user = new CookieUser
                {
                    UserId = response.user.UserId,
                    UserFullName = response.user.UserFullName,
                    Username = response.user.Username,
                };

                CookieOptions options = new CookieOptions();
                options.Secure = false;
                options.Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_appSettings.jwt.ExpiryInMinutes));

                string userProfile = JsonConvert.SerializeObject(user);
                string token = _jwtBusiness.generateJwtToken(userProfile);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("token", token, options);
            }

            return response;
        }
        public IActionResult logout()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("token");
            return RedirectToAction("Index", "Login");
        }
    }
}