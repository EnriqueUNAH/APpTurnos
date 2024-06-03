using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Front.Controllers
{
    public class TestController : Controller
    {
        // GET: TestController
        public ActionResult Index()
        {
            ViewBag.Usuario = HttpContext.Session.GetString("Usuario");
            ViewBag.Nombre = HttpContext.Session.GetString("Nombre");
            ViewBag.Correo = HttpContext.Session.GetString("Correo");
            ViewBag.IDUsuario = HttpContext.Session.GetString("IDUsuario");
            ViewBag.Rol = HttpContext.Session.GetString("rol");
            ViewBag.NombreArea = HttpContext.Session.GetString("NombreArea");
            ViewBag.Numero = HttpContext.Session.GetString("Numero");
            ViewBag.Extension = HttpContext.Session.GetString("Extension");
            ViewBag.NombreZona = HttpContext.Session.GetString("nombreZona");
            ViewBag.Celular = HttpContext.Session.GetString("Celular");
            ViewBag.Estado = HttpContext.Session.GetString("Estado");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string idUsuario, string usuario, string nombre, string rol, string nombreArea, string numero, string extension, string nombreZona, string celular, int estado, string correo)
        {
            var updatedUser = new
            {
                idUsuario,
                usuario,
                nombre,
                rol,
                nombreArea,
                numero,
                extension,
                nombreZona,
                celular,
                estado,
                correo
            };

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(updatedUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;
                try
                {
                    response = await client.PutAsync($"https://localhost:7266/api/Usuario/{idUsuario}", content);
                }
                catch (HttpRequestException e)
                {
                    ViewBag.ErrorMessage = $"Error en la solicitud HTTP: {e.Message}";
                    return View("Index");
                }

                if (response.IsSuccessStatusCode)
                {
                    // Update session with new values
                    HttpContext.Session.SetString("Usuario", usuario);
                    HttpContext.Session.SetString("Nombre", nombre);
                    HttpContext.Session.SetString("Correo", correo);
                    HttpContext.Session.SetString("IDUsuario", idUsuario);
                    HttpContext.Session.SetString("rol", rol);
                    HttpContext.Session.SetString("NombreArea", nombreArea);
                    HttpContext.Session.SetString("Numero", numero);
                    HttpContext.Session.SetString("Extension", extension);
                    HttpContext.Session.SetString("nombreZona", nombreZona);
                    HttpContext.Session.SetString("Celular", celular);
                    HttpContext.Session.SetString("Estado", estado.ToString());

                    return RedirectToAction("Index");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = $"Error updating user: {response.ReasonPhrase}. Detalle: {responseContent}";
                }
            }

            return View("Index");
        }
    }
}
