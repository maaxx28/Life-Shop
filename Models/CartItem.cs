global using Microsoft.Data.SqlClient;

namespace LifeShop.Models
{
    public class CartItem
    {
        private SqlConnection Connection = new(ConnectionStrings.local);

        public int ID { get; private set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public CartItem(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM CartItem WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                CartID = theReader.GetInt32(1);
                ProductID = theReader.GetInt32(2);
                Quantity = theReader.GetInt32(3);
                Price = theReader.GetInt32(4);
            }
            else
            {
                CartID = 0;
                ProductID = 0;
                Quantity = 0;
                Price = 0;
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("CartItemUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@SessionID", CartID);
            theCommand.Parameters.AddWithValue("ProductID", ProductID);
            theCommand.Parameters.AddWithValue("@Quantity", Quantity);
            theCommand.Parameters.AddWithValue("@TotalCost", Price);

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
            SqlCommand theCommand = new("DELETE FROM CartItem WHERE ID=" + id + ";", staticConnection);
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
        public static List<CartItem> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<CartItem> list = new();
            SqlCommand theCommand = new("SELECT ID From CartItem;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new CartItem(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
