using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class BillsRepositry
    {
        private readonly IConfiguration configuration;

        public BillsRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public List<BillsModel> BillDetail()
        {
            var bills = new List<BillsModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bill_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bills.Add(new BillsModel
                {
                    BillID = Convert.ToInt32(reader["BillID"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    UserName = (reader["UserName"]).ToString(),
                    BillNumber = (reader["BillNumber"]).ToString(),
                    BillDate = Convert.ToDateTime(reader["BillDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Discount = Convert.ToDecimal(reader["Discount"]),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"])

                });
            }
            connection.Close();
            return bills;
        }

        public bool InsertBill(BillsModel billsModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bill_Insert";

            command.Parameters.Add("@BillNumber", SqlDbType.VarChar).Value = billsModel.BillNumber;
            command.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = billsModel.BillDate;
            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = billsModel.OrderID;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = billsModel.UserID;
            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billsModel.TotalAmount;
            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = billsModel.Discount;
            command.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = billsModel.NetAmount;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateBill(BillsModel billsModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bill_Update";

            command.Parameters.Add("BillID", SqlDbType.Int).Value = billsModel.BillID;
            command.Parameters.Add("@BillNumber", SqlDbType.VarChar).Value = billsModel.BillNumber;
            command.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = billsModel.BillDate;
            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = billsModel.OrderID;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = billsModel.UserID;
            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = billsModel.TotalAmount;
            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = billsModel.Discount;
            command.Parameters.Add("@NetAmount", SqlDbType.Decimal).Value = billsModel.NetAmount;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteBill(int BillID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bill_Delete";
            command.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public BillsModel BillById(int BillID)
        {
            BillsModel? bills = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bill_SelectByPrimaryKey";
            command.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                bills = new BillsModel
                {
                    BillID = Convert.ToInt32(reader["BillID"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    BillNumber = (reader["BillNumber"]).ToString(),
                    BillDate = Convert.ToDateTime(reader["BillDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    Discount = Convert.ToDecimal(reader["Discount"]),
                    NetAmount = Convert.ToDecimal(reader["NetAmount"])
                };
            }
            connection.Close();
            return bills;
        }
    }
}
