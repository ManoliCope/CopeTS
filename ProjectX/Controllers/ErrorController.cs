using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }

        public IActionResult Index()
        {
            return PartialView("Error");
        }
    }
}
