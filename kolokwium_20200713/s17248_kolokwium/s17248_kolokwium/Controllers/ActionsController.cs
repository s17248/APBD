using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using s17248_kolokwium.DTOs.Requests;
using s17248_kolokwium.DTOs.Responses;
using s17248_kolokwium.Services;

namespace s17248_kolokwium.Controllers
{
    [Route("api/actions")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public ActionsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpPost("{id}/fire-trucks")]
        public IActionResult AssignFiretruckToAction(int id)
        {
            if (_dbService.GetAction(id) == null)
                return BadRequest("Akcja o podanym numerze nie istnieje");

            ActionFiretruckResponse response = _dbService.AssignFiretruckToAction(id, "2020-07-13");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(response, options);
            return Ok(jsonString);
        }
    }
}

