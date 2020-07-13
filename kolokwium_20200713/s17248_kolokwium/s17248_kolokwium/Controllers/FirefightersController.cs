using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using s17248_kolokwium.Services;

namespace s17248_kolokwium.Controllers
{
    [Route("api/firefighters")]
    [ApiController]
    public class FirefightersController : ControllerBase
    {
        private readonly IDbService _dbService;
        public FirefightersController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet("{id}/actions")]
        public IActionResult GetFirefigtherActions(int id)
        {
            if (_dbService.GetFirefighter(id) == null)
                return NotFound("Strazak o podanym numerze nie istnieje");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(_dbService.GetFirefigtherActions(id), options);
            return Ok(jsonString);
        }
    }
}
