using Front.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Front.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;

		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		public IActionResult Index()
		{
			if (HttpContext.Session.GetString("Usuario") == null)
			{
				return RedirectToAction("Index", "Login");
			}

            ViewData["IDUsuario"] = HttpContext.Session.GetString("IDUsuario");
            return View();
		}

		public IActionResult Dashboard()
		{
			if (HttpContext.Session.GetString("Usuario") == null)
			{
				return RedirectToAction("Index", "Login");
			}

            ViewData["IDUsuario"] = HttpContext.Session.GetString("IDUsuario");
            return View();
		}

		public IActionResult Privacy()
		{
			if (HttpContext.Session.GetString("Usuario") == null)
			{
				return RedirectToAction("Index", "Login");
			}

			return View();
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<IActionResult> Salir()
		{
			// Limpiar la sesi�n
			HttpContext.Session.Clear();

			// Cerrar la sesi�n de autenticaci�n
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Redirigir a la p�gina de inicio de sesi�n
			return RedirectToAction("Index", "Login");
		}
	}

}
