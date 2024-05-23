using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
