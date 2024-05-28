using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Front.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string usuario, string password)
        {
            using (var client = new HttpClient())
            {
                // Ajusta la URL según tu API
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
                            // Usuario activo
                            if (user["correo"].ToString() != password) // Cambia esta validación una vez tengas la lógica correcta
                            {
                                // Guardar datos en sesión
                                HttpContext.Session.SetString("Usuario", usuario);
                                HttpContext.Session.SetString("Nombre", user["nombre"].ToString());
                                HttpContext.Session.SetString("Correo", user["correo"].ToString());

                                // Redirigir al home
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
            }

            return View("Index");
        }
    }
}
