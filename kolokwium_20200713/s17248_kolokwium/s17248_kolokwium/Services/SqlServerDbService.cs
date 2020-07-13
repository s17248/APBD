using s17248_kolokwium.DTOs.Requests;
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
        private const string conString = "Data Source=DESKTOP-D0NAD3I\\SQLEXPRESS;Initial Catalog=s17248;Integrated Security=True";
        public FirefighterResponse GetFirefighter(int IdFirefighter)
        {
            string queryString = "SELECT * FROM Firefighter f WHERE f.IdFirefighter=@id";
            FirefighterResponse f = null;

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", IdFirefighter);
                var dr = command.ExecuteReader();
                while (dr.Read()) {
                    f = new FirefighterResponse(
                        Convert.ToInt32(dr["IdFirefighter"]),
                        dr["FirstName"].ToString(),
                        dr["LastName"].ToString()
                    );
                }
            }
            return f;
        }

        public IEnumerable<FirefighterActionResponse> GetFirefigtherActions(int IdFirefighter)
        {
            List<FirefighterActionResponse> listOfActions = new List<FirefighterActionResponse>();
            string queryString = "SELECT a.IdAction, a.StartTime, a.EndTime FROM Action a JOIN Firefighter_Action f ON a.IdAction = f.IdAction WHERE f.IdFirefighter=@id ORDER BY a.EndTime DESC";

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
                        Convert.ToDateTime(dr["StartTime"]),
                        Convert.ToDateTime(dr["EndTime"])
                    );
                    listOfActions.Add(a);
                }
            }
            return listOfActions;
        }
        public ActionResponse GetAction(int id)
        {
            string queryString = "SELECT * FROM Action f WHERE f.IdAction=@id";
            ActionResponse a = null;

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    a = new ActionResponse(
                        Convert.ToInt32(dr["IdAction"]),
                        Convert.ToDateTime(dr["StartTime"]),
                        Convert.ToDateTime(dr["EndTime"]),
                        Convert.ToBoolean(dr["NeedSpecialEquipment"])
                    );
                }
            }
            return a;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdAction"></param>
        /// <returns></returns>
        public ActionFiretruckResponse AssignFiretruckToAction(int IdAction, String data)
        {
            ActionFiretruckResponse response = null;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();

                // https://docs.microsoft.com/pl-pl/dotnet/api/system.data.sqlclient.sqlconnection.begintransaction?view=dotnet-plat-ext-3.1
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction("transaction");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "SELECT * FROM Action f WHERE f.IdAction=@id";
                    command.Parameters.AddWithValue("@id", IdAction);
                    SqlDataReader dr = command.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        transaction.Rollback();
                        throw new ArgumentException("Akcja o podanym numerze nie istnieje");
                    }
                    bool needsSpecialEquipment = (bool)dr["NeedSpecialEquipment"];
                    dr.Close();

                    command.CommandText = "SELECT* FROM FireTruck WHERE IdFireTruck NOT IN(SELECT IdFireTruck FROM FireTruck_Action WHERE CONVERT(date, AssignmentDate) = @data);";
                    //String currentDate = '2020-07-13';
                    command.Parameters.AddWithValue("data", data);
                    dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        response = new ActionFiretruckResponse();
                        response.IdAction = IdAction;
                        response.IdFireTruck = Convert.ToInt32(dr["IdFireTruck"]);
                        dr.Close();
                        command.Parameters.Clear();

                        command.CommandText = "INSERT INTO FireTruck_Action (IdFireTruckAction, IdFireTruck, IdAction, AssignmentDate) VALUES ((SELECT ISNULL(MAX(IdFireTruckAction)+1, 1) FROM FireTruck_Action), @truck, @action, @data);";
                        command.Parameters.AddWithValue("truck", response.IdFireTruck);
                        command.Parameters.AddWithValue("action", response.IdAction);
                        command.Parameters.AddWithValue("data", data+ " 00:00:00 AM");
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                        transaction.Commit();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e);
                    transaction.Rollback();
                    connection.Close();
                    throw e;
                }
                catch (ArgumentException e)
                {
                    connection.Close();
                    throw e;
                }
            }
            return response;
        }
    }
}
