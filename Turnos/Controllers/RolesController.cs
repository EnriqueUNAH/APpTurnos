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
    public class RolesController : ControllerBase
    {
        private readonly RolesData _RolesData;

        public RolesController(RolesData RolesData)
        {
            _RolesData = RolesData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<RolesModel> lista = await _RolesData.Lista();
            return StatusCode(StatusCodes.Status200OK, lista);
        }
    }
}
