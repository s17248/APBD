using System;
using Microsoft.AspNetCore.Mvc;
using APBD.DAL;
using APBD.DTOs.Requests;
using APBD.DTOs.Responses;
using APBD.Models;

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

            // Zapisanie studenta na 1-szy rok
            Enrollment enrollment = null;
            try
            {
                enrollment = _service.EnrollStudent(request);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Created("", enrollment); // Zwraca HTTP 201 z headerem Location="" i Body=enrollment
        }
    }
}



//Student2 st = new Student2();
//st.IndexNumber = request.IndexNumber;
//st.FirstName = request.FirstName;
//st.LastName = request.LastName;
//st.BirthDate = request.Birthdate;

//if (_service.GetStudiesCount(request.Studies) < 1)
//{
//    return BadRequest("Niepoprawna nazwa studiow");
//}

//_service.EnrollStudent(request);
//var response = new EnrollStudentResponse();
////response.LastName = st.LastName;
////...