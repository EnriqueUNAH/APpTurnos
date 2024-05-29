using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

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
                                HttpContext.Session.SetString("Usuario", usuario);
                                HttpContext.Session.SetString("Nombre", user["nombre"].ToString());
                                HttpContext.Session.SetString("Correo", user["correo"].ToString());
                                HttpContext.Session.SetString("IDUsuario", user["idUsuario"].ToString());
                                HttpContext.Session.SetString("rol", user["rol"].ToString());
                                HttpContext.Session.SetString("NombreArea", user["nombreArea"].ToString());
                                HttpContext.Session.SetString("Numero", user["numero"].ToString());
                                HttpContext.Session.SetString("Extension", user["extension"].ToString());
                                HttpContext.Session.SetString("nombreZona", user["nombreZona"].ToString());
                                HttpContext.Session.SetString("Celular", user["celular"].ToString());
                                HttpContext.Session.SetString("Estado", user["estado"].ToString());

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
