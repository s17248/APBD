using System;

namespace APBD.Models
{
    public class Student2
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Student2() { }
        public Student2(string indexnumber, string firstname, string lastname, DateTime dob)
        {
            IndexNumber = indexnumber;
            FirstName = firstname;
            LastName = lastname;
            BirthDate = dob;
        }
    }
}
