using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult AssignFiretruckToAction(int id, FiretruckRequest request)
        {
            if (_dbService.GetAction(id) == null)
                return BadRequest("Akcja o podanym numerze nie istnieje");

            FiretruckResponse f = null;
            try
            {
                f = _dbService.AssignFiretruckToAction(id, request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created("", f);
        }
    }
}

