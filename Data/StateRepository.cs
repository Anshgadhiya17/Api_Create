using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class StateRepositry
    {
        private readonly IConfiguration configuration;

        public StateRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [HttpGet]
        public List<StateModel> StateDetail()
        {
            var states = new List<StateModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                states.Add(new StateModel
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    StateName = (reader["StateName"]).ToString(),
                    StateCode = (reader["StateCode"]).ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"]),
                    CityCount = reader.GetInt32(reader.GetOrdinal("CityCount"))
                });
            }
            connection.Close();
            return states;
        }
        [HttpGet]
        public List<StateDropDownModel> SelectComboBoxByCountryID(int CountryID)
        {
            var state = new List<StateDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_DropDownByCountryID";
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                state.Add(new StateDropDownModel
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = (reader["StateName"]).ToString()
                });
            }
            connection.Close();
            return state;
        }

        public bool InsertState(StateModel stateModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_Insert";

            command.Parameters.Add("@StateName", SqlDbType.VarChar).Value = stateModel.StateName;
            command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = stateModel.StateCode;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = stateModel.CountryID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        [HttpGet]
        public List<StateDropDownModel> StateDropDown()
        {
            var state = new List<StateDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_DropDown";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                state.Add(new StateDropDownModel
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = (reader["StateName"]).ToString()
                });
            }
            connection.Close();
            return state;
        }
        public StateModel StateById(int StateID)
        {
            StateModel? states = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_SelectByPK";
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                states = new StateModel
                {
                    StateID = Convert.ToInt32(reader["StateID"]),
                    StateName = reader["StateName"].ToString(),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    StateCode = (reader["StateCode"]).ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"])
                };
            }
            connection.Close();
            return states;
        }
        public bool UpdateState(StateModel stateModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_Update";

            command.Parameters.Add("@StateID", SqlDbType.Int).Value = stateModel.StateID;
            command.Parameters.Add("@StateName", SqlDbType.VarChar).Value = stateModel.StateName;
            command.Parameters.Add("@StateCode", SqlDbType.VarChar).Value = stateModel.StateCode;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = stateModel.CountryID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteState(int StateID)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_State_Delete";
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
