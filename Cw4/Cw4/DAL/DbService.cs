using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Cw4.Models;

namespace Cw4.DAL
{
    public class DbService : IDbService
    {
        static DbService()
        { }
        public IEnumerable<Student> GetStudents()
        {
            List<Student> listOfStudents = new List<Student>();
            const string conString = "Data Source=DESKTOP-MKBHL3V;Initial Catalog=s17248;Integrated Security=True";
            const string queryString = "SELECT Student.FirstName AS imie, Student.LastName AS nazwisko, Student.BirthDate AS dataUrodzenia, Studies.Name AS nazwaStudiow, Enrollment.IdEnrollment AS numerSemestru FROM Student, Enrollment, Studies WHERE Student.IdEnrollment=Enrollment.IdEnrollment AND Enrollment.IdStudy=Studies.IdStudy;";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                //Console.WriteLine("Połączenie OK");
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Student st = new Student(
                        dr["imie"].ToString(),
                        dr["nazwisko"].ToString(),
                        Convert.ToDateTime(dr["dataUrodzenia"]),
                        dr["nazwaStudiow"].ToString(),
                        Convert.ToInt32(dr["numerSemestru"])
                    );
                    listOfStudents.Add(st);
                }
            }
            //Console.WriteLine("lista = " + listOfStudents.Count());
            return listOfStudents;
        }
        // Zad.4.4
        // s17248') = e.IdEnrollment; DROP TABLE Test;--
        //public IEnumerable<Enrollment> GetStudentEnrollments(string id)
        //{
        //    List<Enrollment> listOfEnrollments = new List<Enrollment>();
        //    const string conString = "Data Source=DESKTOP-MKBHL3V;Initial Catalog=s17248;Integrated Security=True";
        //    string queryString = "SELECT e.IdEnrollment, e.Semester, e.StartDate, s.Name AS StudiesName FROM Enrollment e INNER JOIN Studies s ON e.IdStudy=s.IdStudy WHERE (SELECT st.IdEnrollment FROM Student st WHERE st.IndexNumber = '"+id+"') = e.IdEnrollment";
        //    //Console.WriteLine("queryString: " + queryString);
        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(queryString, connection))
        //    {
        //        connection.Open();
        //        //Console.WriteLine("Połączenie OK");
        //        var dr = command.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            Enrollment e = new Enrollment(
        //                Convert.ToInt32(dr["IdEnrollment"]),
        //                Convert.ToInt32(dr["Semester"]),
        //                Convert.ToDateTime(dr["StartDate"]),
        //                dr["StudiesName"].ToString()
        //            );
        //            listOfEnrollments.Add(e);
        //        }
        //    }
        //    //Console.WriteLine("lista = " + listOfEnrollments.Count());
        //    return listOfEnrollments;
        //}

        // Zad.4.5
        public IEnumerable<Enrollment> GetStudentEnrollments(string id)
        {
            List<Enrollment> listOfEnrollments = new List<Enrollment>();
            const string conString = "Data Source=DESKTOP-MKBHL3V;Initial Catalog=s17248;Integrated Security=True";
            string queryString = "SELECT e.IdEnrollment, e.Semester, e.StartDate, s.Name AS StudiesName FROM Enrollment e INNER JOIN Studies s ON e.IdStudy=s.IdStudy WHERE (SELECT st.IdEnrollment FROM Student st WHERE st.IndexNumber = @id) = e.IdEnrollment";
            //Console.WriteLine("queryString: " + queryString);
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                //Console.WriteLine("Połączenie OK");
                command.Parameters.AddWithValue("@id", id);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Enrollment e = new Enrollment(
                        Convert.ToInt32(dr["IdEnrollment"]),
                        Convert.ToInt32(dr["Semester"]),
                        Convert.ToDateTime(dr["StartDate"]),
                        dr["StudiesName"].ToString()
                    );
                    listOfEnrollments.Add(e);
                }
            }
            //Console.WriteLine("lista = " + listOfEnrollments.Count());
            return listOfEnrollments;
        }

    }
}
