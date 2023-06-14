namespace LifeShop.Models
{
    public class ShoppingCart
    {
        private SqlConnection Connection = new(ConnectionStrings.local);

        public int ID { get; private set; }
        public int CustomerID { get; set; }
        public int TotalItems { get; set; }
        public int TotalCost { get; set; }
        public ShoppingCart(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM ShoppingCart WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                CustomerID = theReader.GetInt32(1);
                TotalItems = theReader.GetInt32(2);
                TotalCost = theReader.GetInt32(3);
            }
            else
            {
                CustomerID = 0;
                TotalItems = 0;
                TotalCost = 0;
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("ShoppingCartUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
            theCommand.Parameters.AddWithValue("@TotalItems", TotalItems);
            theCommand.Parameters.AddWithValue("@TotalCost", TotalCost);


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
            SqlCommand theCommand = new("DELETE FROM ShoppingCart WHERE ID=" + id + ";", staticConnection);
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
        public static List<ShoppingCart> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<ShoppingCart> list = new();
            SqlCommand theCommand = new("SELECT ID From ShoppingCart;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new ShoppingCart(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }

}
