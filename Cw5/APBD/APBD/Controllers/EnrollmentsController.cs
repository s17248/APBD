using System;
using Microsoft.AspNetCore.Mvc;
using APBD.DAL;
using APBD.DTOs.Requests;
using APBD.DTOs.Responses;

namespace APBD.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private IDbService _service;

        public EnrollmentsController(IDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)

        {
            // Walidacja danych
            if (request.IndexNumber == null || request.FirstName == null ||
                request.LastName == null || request.Birthdate == null ||
                request.Studies == null)
            {
                return BadRequest("Brak wymaganych danych");
            }
            if (_service.GetStudiesCount(request.Studies) < 1)
            {
                return BadRequest("Niepoprawna nazwa studiow");
            }

            _service.EnrollStudent(request);
            var response = new EnrollStudentResponse();
            //response.LastName = st.LastName;
            //...

            return Ok(response);
        }
    }
}
