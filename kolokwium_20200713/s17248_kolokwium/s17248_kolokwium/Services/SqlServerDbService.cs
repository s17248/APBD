using s17248_kolokwium.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.Services
{
    public class SqlServerDbService : IDbService
    {

        public FirefighterResponse GetFirefighter(int IdFirefighter)
        {
            const string conString = "Data Source = DESKTOP - D0NAD3I; Initial Catalog = s17248; Integrated Security = True";
            string queryString = "SELECT * FROM Firefighter f WHERE f.IdFirefighter=@id";
            FirefighterResponse f = null;

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", IdFirefighter);
                var dr = command.ExecuteReader();
                f = new FirefighterResponse(
                    Convert.ToInt32(dr["IdFirefighter"]),
                    dr["FirstName"].ToString(),
                    dr["LastName"].ToString()
                );
            }
            return f;
        }

        public IEnumerable<FirefighterActionResponse> GetFirefigtherActions(int IdFirefighter)
        {
            List<FirefighterActionResponse> listOfActions = new List<FirefighterActionResponse>();
            const string conString = "Data Source = DESKTOP - D0NAD3I; Initial Catalog = s17248; Integrated Security = True";
            string queryString = "SELECT a.IdAction, a.StartTime, a.EndTime FROM Action a JOIN Firefighter_Action f ON a.IdAction = f.IdAction WHERE f.IdFirefighter=@id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", IdFirefighter);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    FirefighterActionResponse a = new FirefighterActionResponse(
                        Convert.ToInt32(dr["IdAction"]),
                        Convert.ToDateTime(dr["StartDate"]),
                        Convert.ToDateTime(dr["EndDate"])
                    );
                    listOfActions.Add(a);
                }
            }
            return listOfActions;
        }
    }
}
