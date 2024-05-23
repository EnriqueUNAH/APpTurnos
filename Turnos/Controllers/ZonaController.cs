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
    public class ZonaController : ControllerBase
    {
        private readonly ZonaData _ZonaData;

        public ZonaController(ZonaData ZonaData)
        {
            _ZonaData = ZonaData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<ZonaModels> lista = await _ZonaData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
    }
}
