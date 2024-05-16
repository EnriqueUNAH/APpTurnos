using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Turnos.Data;
using Turnos.Models;
namespace Turnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly usuariosData _usuariosData;
        public UsuarioController(usuariosData usuariosData)
        {
            _usuariosData = usuariosData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<UsuariosModel> Lista = await _usuariosData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            UsuariosModel objeto = await _usuariosData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuariosModel objeto)
        {
            bool respuesta = await _usuariosData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] UsuariosModel objeto)
        {
            bool respuesta = await _usuariosData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _usuariosData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
