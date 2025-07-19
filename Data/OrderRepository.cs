using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class OrderRepositry
    {
        private readonly IConfiguration configuration;

        public OrderRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public List<OrderModel> OrderDetail()
        {
            var Order = new List<OrderModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order.Add(new OrderModel
                {
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    PaymentMode = (reader["PaymentMode"]).ToString(),
                    ShippingAddress = (reader["ShippingAddress"]).ToString(),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"])

                });
            }
            connection.Close();
            return Order;
        }

        public bool InsertOrder(OrderModel orderModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_Insert";

            command.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = orderModel.OrderDate;
            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = orderModel.CustomerID;
            command.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = orderModel.PaymentMode;
            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = orderModel.TotalAmount;
            command.Parameters.Add("@ShippingAddress", SqlDbType.VarChar).Value = orderModel.ShippingAddress;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = orderModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateOrder(OrderModel orderModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_Update";

            command.Parameters.Add("OrderID", SqlDbType.Int).Value = orderModel.OrderID;
            command.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = orderModel.OrderDate;
            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = orderModel.CustomerID;
            command.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = orderModel.PaymentMode;
            command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = orderModel.TotalAmount;
            command.Parameters.Add("@ShippingAddress", SqlDbType.VarChar).Value = orderModel.ShippingAddress;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = orderModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteOrder(int OrderID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_Delete";
            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public OrderModel OrderById(int OrderID)
        {
            OrderModel? Order = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_SelectByPrimaryKey";
            command.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order = new OrderModel
                {
                    OrderID = Convert.ToInt32(reader["OrderID"]),
                    PaymentMode = (reader["PaymentMode"]).ToString(),
                    ShippingAddress = (reader["ShippingAddress"]).ToString(),
                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                    UserID = Convert.ToInt32(reader["UserID"])

                };
            }
            connection.Close();
            return Order;
        }

        [HttpGet]
        public List<OrderDropDownModel> OrderDropDown()
        {
            var Order = new List<OrderDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_DropDown";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Order.Add(new OrderDropDownModel
                {
                    OrderID = Convert.ToInt32(reader["OrderID"])

                });
            }
            connection.Close();
            return Order;
        }
    }
}
