using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class UserRepositry
    {
        private readonly IConfiguration configuration;

        public UserRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public List<UserModel> UserDetail()
        {
            var User = new List<UserModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User.Add(new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"]).ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Address = reader["Address"].ToString(),

                });
            }
            connection.Close();
            return User;
        }

        public bool InsertUser(UserModel userModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_Insert";

            command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userModel.UserName;
            command.Parameters.Add("@Address", SqlDbType.VarChar).Value = userModel.Address;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userModel.IsActive;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = userModel.Email;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = userModel.Password;
            command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = userModel.MobileNo;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateUser(UserModel userModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_Update";

            command.Parameters.Add("UserID", SqlDbType.Int).Value = userModel.UserID;
            command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userModel.UserName;
            command.Parameters.Add("@Address", SqlDbType.VarChar).Value = userModel.Address;
            command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = userModel.IsActive;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = userModel.Email;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = userModel.Password;
            command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = userModel.MobileNo;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteUser(int UserID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_Delete";
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public UserModel UserById(int UserID)
        {
            UserModel? User = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectByPrimaryKey";
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User = new UserModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"]).ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Address = reader["Address"].ToString(),

                };
            }
            connection.Close();
            return User;
        }

        [HttpGet]
        public List<UserDropDownModel> UserDropDown()
        {
            var User = new List<UserDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_DropDown";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User.Add(new UserDropDownModel
                {
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"]).ToString()

                });
            }
            connection.Close();
            return User;
        }
    }
}
