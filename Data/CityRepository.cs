using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class CityRepositry
    {
        private readonly IConfiguration configuration;

        public CityRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [HttpGet]
        public List<CityModel> CityDetail()
        {
            var cities = new List<CityModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cities.Add(new CityModel
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CityName = (reader["CityName"]).ToString(),
                    CityCode = (reader["CityCode"]).ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"])
                });
            }
            connection.Close();
            return cities;
        }

        [HttpPost]

        public bool Insert(CityModel city)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_Insert";
            command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = city.CityName;
            command.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = city.CityCode;
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = city.StateID;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = city.CountryID;
            //command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            //command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateCity(CityModel cityModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_Update";

            command.Parameters.Add("CityID", SqlDbType.Int).Value = cityModel.CityID;
            command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = cityModel.CityName;
            command.Parameters.Add("@CityCode", SqlDbType.VarChar).Value = cityModel.CityCode;
            command.Parameters.Add("@StateID", SqlDbType.Int).Value = cityModel.StateID;
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = cityModel.CountryID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteCity(int CityID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_Delete";
            command.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public CityModel CityById(int CityID)
        {
            CityModel? cities = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_City_SelectByPK";
            command.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                cities = new CityModel
                {
                    CityID = Convert.ToInt32(reader["CityID"]),
                    StateID = Convert.ToInt32(reader["StateID"]),
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CityName = (reader["CityName"]).ToString(),
                    CityCode = (reader["CityCode"]).ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"])
                };
            }
            connection.Close();
            return cities;
        }


    }
}
