using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> ActualizarParcial(int id, [FromBody] JsonPatchDocument<UsuariosModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            UsuariosModel usuarioActual = await _usuariosData.Obtener(id);
            if (usuarioActual == null)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var camposActualizados = new Dictionary<string, object>();
            foreach (var op in patchDoc.Operations)
            {
                camposActualizados[op.path.TrimStart('/')] = op.value;
            }

            bool respuesta = await _usuariosData.ActualizarParcial(id, camposActualizados);

            if (!respuesta)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el usuario.");
            }

            return Ok(new { isSuccess = respuesta });
        }



    }
}
