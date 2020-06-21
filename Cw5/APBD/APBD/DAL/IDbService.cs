using System;
using APBD.Models;
using System.Collections.Generic;
using APBD.DTOs.Requests;
using APBD.DTOs.Responses;

namespace APBD.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Enrollment> GetStudentEnrollments(string id);
        public int GetStudiesCount(string studiesName);
        public Enrollment EnrollStudent(EnrollStudentRequest request);
        //public void PromoteStudents(int semester, string studies);
    }
}
