using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class OrderDetailRepositry
    {
        private readonly IConfiguration configuration;

        public OrderDetailRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public List<OrderDetailModel> OrderDetail()
        {
            var OrderDetail = new List<OrderDetailModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                OrderDetail.Add(new OrderDetailModel
                {
                    OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"]),

                });
            }
            connection.Close();
            return OrderDetail;
        }

        public bool InsertOrderDetail(OrderDetailModel orderDetailModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_Insert";

            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderDetailModel.OrderID;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = orderDetailModel.ProductID;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = orderDetailModel.Quantity;
            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = orderDetailModel.Amount;
            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = orderDetailModel.TotalAmount;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = orderDetailModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateOrderDetail(OrderDetailModel orderDetailModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_Update";

            command.Parameters.Add("OrderDetailID", SqlDbType.Int).Value = orderDetailModel.OrderDetailID;
            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = orderDetailModel.OrderID;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = orderDetailModel.ProductID;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = orderDetailModel.Quantity;
            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = orderDetailModel.Amount;
            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = orderDetailModel.TotalAmount;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = orderDetailModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteOrderDetail(int OrderDetailID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_Delete";
            command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = OrderDetailID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public OrderDetailModel OrderDetailById(int OrderDetailID)
        {
            OrderDetailModel? OrderDetail = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectByPrimaryKey";
            command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = OrderDetailID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                OrderDetail = new OrderDetailModel
                {
                    OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    Quantity = Convert.ToInt32(reader["Quantity"]),
                    Amount = Convert.ToDecimal(reader["Amount"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"]),

                };
            }
            connection.Close();
            return OrderDetail;
        }
    }
}
