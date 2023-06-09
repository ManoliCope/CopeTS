using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    public class LandingController : SharedController
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAppointment()
        {
            string IP = HttpContext.Connection.RemoteIpAddress.ToString();
            return Ok();
        }

    }
}
