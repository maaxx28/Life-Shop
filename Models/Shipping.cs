namespace LifeShop.Models
{
    public class Shipping
    {
        private SqlConnection Connection = new(ConnectionStrings.local);

        public int ID { get; private set; }
        public int OrderID { get; set; }
        public string Status { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipState { get; set; }
        public int ShipZip { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Shipping(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM Shipping WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                OrderID = theReader.GetInt32(1);
                Status = theReader.GetString(2);
                ShipAddress = theReader.GetString(3);
                ShipCity = theReader.GetString(4);
                ShipState = theReader.GetString(5);
                ShipZip = theReader.GetInt32(6);
                DeliveryDate = theReader.GetDateTime(7);
            }
            else
            {
                OrderID = 0;
                Status = "";
                ShipAddress = "";
                ShipCity = "";
                ShipState = "";
                ShipZip = 00000;
                DeliveryDate = DateTime.Now;
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("ShippingUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@OrderID", OrderID);
            theCommand.Parameters.AddWithValue("@ShippingStatus", Status);
            theCommand.Parameters.AddWithValue("@ShippingAddress", ShipAddress);
            theCommand.Parameters.AddWithValue("@ShippingCity", ShipCity);
            theCommand.Parameters.AddWithValue("@ShipState", ShipState);
            theCommand.Parameters.AddWithValue("ShipZip", ShipZip);
            theCommand.Parameters.AddWithValue("DelivaryDate", DeliveryDate);

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
            SqlCommand theCommand = new("DELETE FROM Shipping WHERE ID=" + id + ";", staticConnection);
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
        public static List<Shipping> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Shipping> list = new();
            SqlCommand theCommand = new("SELECT ID From Shipping;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Shipping(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
