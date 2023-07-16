using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
