using ProjectX.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProjectX.Entities.dbModels;
//using ProjectX.Entities.Models.Avaya;
using Newtonsoft.Json;

namespace ProjectX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public TR_Users _user;

        public HomeController(IHttpContextAccessor context, ILogger<HomeController> logger)
        {
            _user = (TR_Users)context.HttpContext.Items["User"];
            _logger = logger;
        }

        public IActionResult Index(string cid, string cnum, int csid)
        {
            //string username = _user.UserFullName;
            //ViewData["username"] = username;
            //_user.group.pages = _user.group.pages.Where(x => x.GP_AllowInMenu == true).ToList();
            //ViewData["user"] = _user;
            //ViewBag.screentype = "home";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
