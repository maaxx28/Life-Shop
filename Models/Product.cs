namespace LifeShop.Models
{
    public class ConnectionStrings
    {
        public static string local = "Data Source=localhost\\SQLEXPRESS; Integrated Security=true; Initial Catalog=LifeShopData;";
    }

    public class Product
    {
        private SqlConnection Connection = new(ConnectionStrings.local);
        public int ID { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public decimal Discount { get; set; }

        public Product(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM Product WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                Name = theReader.GetString(1);
                Description = theReader.GetString(2);
                Price = theReader.GetDecimal(3);
                Picture = theReader.GetString(4);
                Discount = theReader.GetInt32(5);
            }
            else
            {
                Name = string.Empty;
                Description = string.Empty;
                Price = 0;
                Picture = "";
                Discount = 0;
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("ProductUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@ItemName", Name);
            theCommand.Parameters.AddWithValue("@ItemDesc", Description);
            theCommand.Parameters.AddWithValue("@Price", Price);
            theCommand.Parameters.AddWithValue("@Picture", Picture);
            theCommand.Parameters.AddWithValue("@Discount", Discount);

            SqlParameter newParameter = new("@NewID", 0);
            newParameter.Direction = System.Data.ParameterDirection.Output;
            theCommand.Parameters.Add(newParameter);
            String message = "The row was successfully updated.";
            bool success = false;
            try
            {
                Connection.Open();
                theCommand.ExecuteNonQuery();
                if (ID == 0)
                {
                    ID = Convert.ToInt32(theCommand.Parameters["@NewID"].Value);
                    success = true;
                    message = "Success, the ID of the new record is " + ID;
                }
            }
            catch (Exception ex)
            {
                message = "The row was not successfully updated. Error: " + ex.Message;
            }
            finally
            {
                Connection.Close();
            }

            return message;
        }
        public static void Delete(int id)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("DELETE FROM Product WHERE ID=" + id + ";", staticConnection);
            staticConnection.Open();
            try
            {
                theCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                String theMessage = ex.Message;
            }
            finally
            {
                staticConnection.Close();
            }
        }
        public static List<Product> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Product> list = new();
            SqlCommand theCommand = new("SELECT ID From Product;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Product(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }

    }
}
