using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using Turnos.Data;
using Turnos.Models;
namespace Turnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : Controller
    {
        private readonly AsignacionesData _AsignacionesData;
        public AsignacionesController(AsignacionesData AsignacionesData)
        {
            _AsignacionesData = AsignacionesData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<AsignacionesModels> Lista = await _AsignacionesData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            AsignacionesModels objeto = await _AsignacionesData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AsignacionesModels objeto)
        {
            bool respuesta = await _AsignacionesData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] AsignacionesModels objeto)
        {
            bool respuesta = await _AsignacionesData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _AsignacionesData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
    }
}
