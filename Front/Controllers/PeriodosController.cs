using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class PeriodosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
