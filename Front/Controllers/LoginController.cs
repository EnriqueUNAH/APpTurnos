using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace Front.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string usuario, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7266/api/Usuario?usuario={usuario}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var users = JArray.Parse(jsonResponse);

                var user = users.FirstOrDefault(u => u["usuario"].ToString() == usuario);

                if (user != null)
                {
                    if (user["estado"].ToString() == "1")
                    {
                        if (user["estado"].ToString() != password)
                        {
                            // Crear los claims
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user["nombre"].ToString()),
                                new Claim(ClaimTypes.Email, user["correo"].ToString()),
                                new Claim("IDUsuario", user["idUsuario"].ToString()),
                                new Claim("Usuario", user["usuario"].ToString()),
                                // Añadir otros claims según sea necesario
                            };

                            // Crear la identidad y el principal
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            // Iniciar sesión
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                            // Guardar en la sesión
                            HttpContext.Session.SetString("Usuario", usuario);
                            HttpContext.Session.SetString("Nombre", user["nombre"].ToString());
                            HttpContext.Session.SetString("Correo", user["correo"].ToString());

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Contraseña incorrecta";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Cuenta inactiva. Comuníquese con los administradores.";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Usuario no encontrado";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Error al verificar el usuario";
            }

            return View("Index");
        }
    }
}
