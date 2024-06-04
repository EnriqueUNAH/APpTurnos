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

		[HttpGet]
		public async Task<IActionResult> GetProfile()
		{
			var idUsuario = HttpContext.Session.GetString("IDUsuario");
			if (string.IsNullOrEmpty(idUsuario))
			{
				return Json(new { success = false });
			}

			var client = _httpClientFactory.CreateClient();
			var response = await client.GetAsync($"https://localhost:7266/api/Usuario/{idUsuario}");
			if (!response.IsSuccessStatusCode)
			{
				return Json(new { success = false });
			}

			var data = await response.Content.ReadAsStringAsync();
			var user = JsonConvert.DeserializeObject<UserProfile>(data);

			return Json(new { success = true, user });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProfile([FromBody] UserProfile model)
		{
			var client = _httpClientFactory.CreateClient();
			var response = await client.PutAsJsonAsync($"https://localhost:7266/api/Usuario/{model.IDUsuario}", model);
			if (!response.IsSuccessStatusCode)
			{
				return Json(new { success = false });
			}

			// Actualiza los claims del usuario
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, model.Nombre),
				new Claim(ClaimTypes.Email, model.Correo),
				new Claim("IDUsuario", model.IDUsuario),
				new Claim("Usuario", model.Usuario),
				new Claim("IDRol", model.IDRol),
				new Claim("IDArea", model.IDArea),
				new Claim("Numero", model.Numero),
				new Claim("Extension", model.Extension),
				new Claim("IdZona", model.IdZona),
				new Claim("Celular", model.Celular),
				new Claim("Estado", model.Estado)
			};

			var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			// Refrescar la sesión
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

			return Json(new { success = true });
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<IActionResult> Salir()
		{
			// Limpiar la sesión
			HttpContext.Session.Clear();

			// Cerrar la sesión de autenticación
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// Redirigir a la página de inicio de sesión
			return RedirectToAction("Index", "Login");
		}
	}

	public class UserProfile
	{
		public string IDUsuario { get; set; }
		public string Usuario { get; set; }
		public string Nombre { get; set; }
		public string IDRol { get; set; }
		public string IDArea { get; set; }
		public string Numero { get; set; }
		public string Extension { get; set; }
		public string IdZona { get; set; }
		public string Celular { get; set; }
		public string Estado { get; set; }
		public string Correo { get; set; }
	}
}
