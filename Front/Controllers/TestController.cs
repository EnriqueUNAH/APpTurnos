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
            ViewBag.IDRol = HttpContext.Session.GetString("IDRol");
            ViewBag.IDArea = HttpContext.Session.GetString("IDArea");
            ViewBag.Numero = HttpContext.Session.GetString("Numero");
            ViewBag.Extension = HttpContext.Session.GetString("Extension");
            ViewBag.IdZona = HttpContext.Session.GetString("IdZona");
            ViewBag.Celular = HttpContext.Session.GetString("Celular");
            ViewBag.Estado = HttpContext.Session.GetString("Estado");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(string idUsuario, string usuario, string nombre, string idRol, string idArea, string numero, string extension, string idZona, string celular, string estado, string correo)
        {
            var updatedUser = new
            {
                idUsuario,
                usuario,
                nombre,
                idRol,
                idArea,
                numero,
                extension,
                idZona,
                celular,
                estado,
                correo
            };

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(updatedUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7266/api/Usuario/{idUsuario}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Update session with new values
                    HttpContext.Session.SetString("Usuario", usuario);
                    HttpContext.Session.SetString("Nombre", nombre);
                    HttpContext.Session.SetString("Correo", correo);
                    HttpContext.Session.SetString("IDUsuario", idUsuario);
                    HttpContext.Session.SetString("Numero", numero);
                    HttpContext.Session.SetString("Extension", extension);
                    HttpContext.Session.SetString("Celular", celular);
                    HttpContext.Session.SetString("Estado", estado);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error updating user";
                }
            }

            return View("Index");
        }
    }
}
