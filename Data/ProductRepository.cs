using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ApiDemo.Data
{
    public class ProductRepositry
    {
        private readonly IConfiguration configuration;

        public ProductRepositry(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [HttpGet]
        public List<ProductModel> ProductDetail()
        {
            var Product = new List<ProductModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product.Add(new ProductModel
                {
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    ProductName = (reader["ProductName"]).ToString(),
                    ProductPrice = Convert.ToInt32(reader["ProductPrice"]),
                    ProductCode = reader["ProductCode"].ToString(),
                    Description = reader["Description"].ToString(),

                });
            }
            connection.Close();
            return Product;
        }

        public bool InsertProduct(ProductModel productModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_Insert";

            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productModel.ProductName;
            command.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = productModel.ProductCode;
            command.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = productModel.ProductPrice;
            command.Parameters.Add("@Description", SqlDbType.VarChar).Value = productModel.Description;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = productModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool UpdateProduct(ProductModel productModel)
        {
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_Update";

            command.Parameters.Add("ProductID", SqlDbType.Int).Value = productModel.ProductID;
            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productModel.ProductName;
            command.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = productModel.ProductCode;
            command.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = productModel.ProductPrice;
            command.Parameters.Add("@Description", SqlDbType.VarChar).Value = productModel.Description;
            command.Parameters.Add("@UserID", SqlDbType.Int).Value = productModel.UserID;
            //command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public bool DeleteProduct(int ProductID)
        {

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_Delete";
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
            command.ExecuteNonQuery();

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public ProductModel ProductById(int ProductID)
        {
            ProductModel? Product = null;
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_SelectByPrimaryKey";
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product = new ProductModel
                {
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    ProductName = (reader["ProductName"]).ToString(),
                    ProductPrice = Convert.ToInt32(reader["ProductPrice"]),
                    ProductCode = reader["ProductCode"].ToString(),
                    Description = reader["Description"].ToString(),
                    UserID = Convert.ToInt32(reader["UserID"])

                };
            }
            connection.Close();
            return Product;
        }

        [HttpGet]
        public List<ProductDropDownModel> ProductDropDown()
        {
            var Product = new List<ProductDropDownModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_DropDown";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product.Add(new ProductDropDownModel
                {
                    ProductID = Convert.ToInt32(reader["ProductID"]),
                    ProductName = (reader["ProductName"]).ToString()

                });
            }
            connection.Close();
            return Product;
        }
    }
}
