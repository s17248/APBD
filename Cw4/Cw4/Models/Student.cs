using System;

namespace Cw4.Models
{
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string NazwaStudiow { get; set; }
        public int NrSemestru { get; set; }
        public Student() { }
        public Student(string firstname, string surname, DateTime dob, string studies, int semester)
        {
            Imie = firstname;
            Nazwisko = surname;
            DataUrodzenia = dob;
            NazwaStudiow = studies;
            NrSemestru = semester;
        }
    }
}
