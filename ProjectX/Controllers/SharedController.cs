using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Infrastructure.Models;
using Newtonsoft.Json;

namespace ProjectX.Controllers
{
    public class SharedController : Controller
    {
        //public static ApplicationUser MyUser = new ApplicationUser();
        public static TransactionsRepository transactionsRepository = new TransactionsRepository();

        // GET: Shared
        public ActionResult Header()
        {
            return PartialView();
        }
        // GET: Shared
       

        //public Usersme checkuser()
        //{
        //    Usersme User = new Usersme();

        //    try
        //    {
        //        if (HttpContext.Session.GetString("SessionUser") == null)
        //        {
        //            return null;

        //        }
        //        else
        //        {
        //            User = JsonConvert.DeserializeObject<Usersme>(HttpContext.Session.GetString("SessionUser"));
        //        }

        //        return User;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}