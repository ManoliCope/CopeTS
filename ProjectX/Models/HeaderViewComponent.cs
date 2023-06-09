using ProjectX.Models;
using ProjectX.Repository.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProjectX.Entities.dbModels;
using ProjectX.Entities;
//using ProjectX.Entities.Models.Avaya;
using Newtonsoft.Json;


namespace ProjectX.Models
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ILogger<HeaderViewComponent> _logger;
        public User _user;

        public HeaderViewComponent(IHttpContextAccessor context, ILogger<HeaderViewComponent> logger)
        {
            _user = (User)context.HttpContext.Items["User"];
            _logger = logger;
        }


        public IViewComponentResult Invoke()
        {
            string username = _user.UserFullName;
            ViewData["username"] = username;
            //_user.group.pages = _user.group.pages.Where(x => x.GP_AllowInMenu == true).ToList();
            ViewData["user"] = _user;
            ViewBag.screentype = "home";
            return View("~/views/Shared/Header.cshtml", username);
        }
    }
}
