using System;

namespace APBD.Models
{
    public class Enrollment
    {
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public string StudiesName { get; set; }
        public Enrollment() { }
        public Enrollment(int id, int semester, DateTime start, string name)
        {
            IdEnrollment = id;
            Semester = semester;
            StartDate = start;
            StudiesName = name;
        }
    }
}
