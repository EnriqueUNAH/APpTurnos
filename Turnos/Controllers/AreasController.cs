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
    public class AreasController : ControllerBase
    {
        private readonly AreasData _AreasData;

        public AreasController(AreasData areasData)
        {
            _AreasData = areasData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<AreasModels> lista = await _AreasData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
    }
}
