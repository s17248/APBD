using APBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APBD.DAL;
using System.Data.SqlClient;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APBD.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        // Zad.4.2
        [HttpGet]
        public IActionResult GetStudents()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(_dbService.GetStudents(), options);
            return Ok(jsonString);
            //return Ok(new JsonResult(_dbService.GetStudents()));
        }

        // Zad.4.3
        [HttpGet("{id}")]
        public IActionResult GetStudentEnrollments(string id)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(_dbService.GetStudentEnrollments(id), options);
            return Ok(jsonString);
            //return Ok(_dbService.GetStudentEnrollments(id));
        }
    }
}
