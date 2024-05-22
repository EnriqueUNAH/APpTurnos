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
    public class UsuarioTurnoController : ControllerBase
    {
        private readonly UsuariosTurnosData _usuariosTurnosData;

        public UsuarioTurnoController(UsuariosTurnosData usuariosTurnosData)
        {
            _usuariosTurnosData = usuariosTurnosData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<usuariosTurnoModel> lista = await _usuariosTurnosData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
    }
}
