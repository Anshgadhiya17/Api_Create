using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class CustomerRepositry
    {
        private readonly IConfiguration configuration;

        public CustomerRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public List<CustomerModel> CustomerDetail()
        {
            var Customer = new List<CustomerModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Customer.Add(new CustomerModel
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    HomeAddress = reader["HomeAddress"].ToString(),
                    Email = reader["Email"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Gst_No = reader["Gst_No"].ToString(),
                    CityName = reader["CityName"].ToString(),
                    PinCode = reader["PinCode"].ToString(),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"])

                });
            }
            connection.Close();
            return Customer;
        }

        public bool InsertCustomer(CustomerModel customerModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_Insert";

            command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customerModel.CustomerName;
            command.Parameters.Add("@HomeAddress", SqlDbType.VarChar).Value = customerModel.HomeAddress;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = customerModel.Email;
            command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customerModel.MobileNo;
            command.Parameters.Add("@Gst_No", SqlDbType.VarChar).Value = customerModel.Gst_No;
            command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = customerModel.CityName;
            command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = customerModel.PinCode;
            command.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = customerModel.NetAmount;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = customerModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateCustomer(CustomerModel customerModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_Update";

            command.Parameters.Add("CustomerID", SqlDbType.Int).Value = customerModel.CustomerId;
            command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customerModel.CustomerName;
            command.Parameters.Add("@HomeAddress", SqlDbType.VarChar).Value = customerModel.HomeAddress;
            command.Parameters.Add("@Email", SqlDbType.VarChar).Value = customerModel.Email;
            command.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = customerModel.MobileNo;
            command.Parameters.Add("@Gst_No", SqlDbType.VarChar).Value = customerModel.Gst_No;
            command.Parameters.Add("@CityName", SqlDbType.VarChar).Value = customerModel.CityName;
            command.Parameters.Add("@PinCode", SqlDbType.VarChar).Value = customerModel.PinCode;
            command.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = customerModel.NetAmount;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = customerModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteCustomer(int CustomerID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_Delete";
            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public CustomerModel CustomerById(int CustomerID)
        {
            CustomerModel? Customer = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectByPrimaryKey";
            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Customer = new CustomerModel
                {
                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString(),
                    HomeAddress = reader["HomeAddress"].ToString(),
                    Email = reader["Email"].ToString(),
                    MobileNo = reader["MobileNo"].ToString(),
                    Gst_No = reader["Gst_No"].ToString(),
                    CityName = reader["CityName"].ToString(),
                    PinCode = reader["PinCode"].ToString(),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"])

                };
            }
            connection.Close();
            return Customer;
        }

        [HttpGet]
        public List<CustomerDropDownModel> CustomerDropDown()
        {
            var Customer = new List<CustomerDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Customer.Add(new CustomerDropDownModel
                {
                    CustomerID = Convert.ToInt32(reader["CustomerId"]),
                    CustomerName = reader["CustomerName"].ToString()

                });
            }
            connection.Close();
            return Customer;
        }
    }
}
