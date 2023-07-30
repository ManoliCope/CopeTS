﻿using ProjectX.Business.Jwt;
using ProjectX.Entities.AppSettings;
using ProjectX.Business.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using ProjectX.Entities.Models.Users;
using ProjectX.Entities;

namespace ProjectX.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUsersBusiness _userBusiness;
        private IJwtBusiness _jwtBusiness;
        private readonly TrAppSettings _appSettings;

        public LoginController(IHttpContextAccessor httpContextAccessor, IJwtBusiness jwtBusiness, IOptions<TrAppSettings> appIdentitySettingsAccessor, IUsersBusiness userBusiness)
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
                response.statusCode.code = 1;
                CookieUser user = new CookieUser
                {
                    UserId = response.user.U_Id,
                    UserFullName = response.user.U_Full_Name,
                    Username = response.user.U_User_Name,
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