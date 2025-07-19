using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class CountryRepositry
    {
        private readonly IConfiguration configuration;

        public CountryRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [HttpGet]
        public List<CountryModel> CountryDetail()
        {
            var countries = new List<CountryModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                countries.Add(new CountryModel
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = (reader["CountryName"]).ToString(),
                    CountryCode = (reader["CountryCode"]).ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"])
                });
            }
            connection.Close();
            return countries;
        }
        public CountryModel CountryByID(int CountryID)
        {
            CountryModel? country = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_SelectByPK";
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                country = new CountryModel
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = reader["CountryName"].ToString(),
                    CountryCode = (reader["CountryCode"]).ToString(),
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"])
                };
            }
            connection.Close();
            return country;
        }
        [HttpGet]
        public List<CountryDropDownModel> CountryDropDown()
        {
            var countries = new List<CountryDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_DropDown";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                countries.Add(new CountryDropDownModel
                {
                    CountryID = Convert.ToInt32(reader["CountryID"]),
                    CountryName = reader["CountryName"].ToString()
                });
            }
            connection.Close();
            return countries;
        }
        public bool InsertCountry(CountryModel countryModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_Insert";
            command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = countryModel.CountryName;
            command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = countryModel.CountryCode;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteCountry(int CountryID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_Delete";
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;

        }

        public bool UpdateCountry(CountryModel countryModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_LOC_Country_Update";
            command.Parameters.Add("@CountryID", SqlDbType.Int).Value = countryModel.CountryID;
            command.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = countryModel.CountryName;
            command.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = countryModel.CountryCode;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
}
