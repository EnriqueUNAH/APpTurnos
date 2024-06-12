using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Turnos.Data;
using Turnos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Turnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodosController : ControllerBase
    {
        private readonly PeriodosData _PeriodosData;

        public PeriodosController(PeriodosData PeriodosData)
        {
            _PeriodosData = PeriodosData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<PeriodosModel> lista = await _PeriodosData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            PeriodosModel objeto = await _PeriodosData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PeriodosModel objeto)
        {
            bool respuesta = await _PeriodosData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] PeriodosModel objeto)
        {
            bool respuesta = await _PeriodosData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _PeriodosData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
        }

    }
}
