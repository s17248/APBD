using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using APBD.DTOs.Requests;
using APBD.Models;

namespace APBD.DAL
{
    public class DbService : IDbService
    {
        static DbService()
        { }
        public IEnumerable<Student> GetStudents()
        {
            List<Student> listOfStudents = new List<Student>();
            //// Visual Studio Express 2019 in VMWare
            //const string conString = "Data Source=DESKTOP-MKBHL3V;Initial Catalog=s17248;Integrated Security=True";
            //// Visual Studio Express 2017 on ThinkPad
            //const string conString = "Data Source=DESKTOP-0EHFFBE;Initial Catalog=s17248;Integrated Security=True";
            // SQL Server in Docker
            const string conString = "Server=127.0.0.1,1401;Database=s17248;User Id=sa;Password=Admin123$";
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

        public int GetStudiesCount(string studiesName)
        {
            List<Student> listOfStudents = new List<Student>();
            const string conString = "Server=127.0.0.1,1401;Database=s17248;User Id=sa;Password=Admin123$";
            string queryString = "SELECT COUNT(*) FROM Studies WHERE Name='"+studiesName+"';";
            Int32 count = -1;
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                count = (Int32)command.ExecuteScalar();
            }
            Console.WriteLine("count = " + count);
            return count;
        }

        public void EnrollStudent(EnrollStudentRequest request)
        {
            //DTOs - Data Transfer Objects
            //Request models
            //==mapowanie==
            //Modele biznesowe/encje (entity)
            //==mapowanie==
            //Response models

            Console.WriteLine("DbService.EnrollStudent() START");
            Console.WriteLine("request="+request);
            Console.WriteLine("request.IndexNumber=" + request.IndexNumber);
            Console.WriteLine("request.FirstName=" + request.FirstName);
            Console.WriteLine("request.LastName=" + request.LastName);
            Console.WriteLine("request.Birthdate=" + request.Birthdate);
            Console.WriteLine("request.Studies=" + request.Studies);
            return;

            var st = new Student();
            st.Imie = request.FirstName;
            st.Nazwisko = request.LastName;
            st.DataUrodzenia = request.Birthdate;
            st.NazwaStudiow = request.Studies;
            st.NrSemestru = 1;

            //...
            //...
            //Micro ORM object-relational mapping
            //problemami - impedance mismatch

            using (var con = new SqlConnection(""))
            using (var com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    //1. Czy studia istnieja?
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //return BadRequest("Studia nie istnieja");
                        //...
                    }
                    int idstudies = (int)dr["IdStudies"];

                    //x. Dodanie studenta
                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName) VALUES(@Index, @Fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    //...
                    com.ExecuteNonQuery();

                    tran.Commit();

                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

        }
    }
}
