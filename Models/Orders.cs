using Microsoft.AspNetCore.Mvc;

namespace LifeShop.Models
{
    public class Order
    {
        private SqlConnection Connection = new(ConnectionStrings.local);

        public int ID { get; private set; }
        public int CustomerID { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime OrderDate { get; set; }
        public int PaymentID { get; set; }
        public Order(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM [Order] WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                CustomerID = theReader.GetInt32(1);
                TotalItems = theReader.GetInt32(2);
                TotalCost = theReader.GetDecimal(3);
                OrderDate = theReader.GetDateTime(4);
                PaymentID = theReader.GetInt32(5);

                Connection.Close();
            }
            else
            {
                CustomerID = 0;
                TotalItems = 0;
                TotalCost = 0;
                OrderDate = DateTime.Now;
                PaymentID = 0;
            }
        }
        public int Save()
        {
            SqlCommand theCommand = new("OrderUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("CustomerID", CustomerID);
            theCommand.Parameters.AddWithValue("@TotalItems", TotalItems);
            theCommand.Parameters.AddWithValue("@TotalCost", TotalCost);
            theCommand.Parameters.AddWithValue("@OrderDate", OrderDate);
            theCommand.Parameters.AddWithValue("@PaymentID", PaymentID);

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
            if(success)
            {
                Shipping theShipment = new Shipping(0);
                theShipment.OrderID = ID;
                theShipment.Status = "Created";
                theShipment.Save();
            }
            return ID;
        }
        public int GetShipment(int id)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT ID FROM Shipping WHERE OrderID='" + id + "';", staticConnection);
            staticConnection.Open();
            int theShipment;
            try
            {
                theShipment = Convert.ToInt32(theCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                theShipment =Convert.ToInt32(null);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                staticConnection.Close();
            }
            return theShipment;
        }
        public static void Delete(int id)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("DELETE FROM [Order] WHERE ID=" + id + ";", staticConnection);
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
        public static List<Order> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Order> list = new();
            SqlCommand theCommand = new("SELECT ID From [Order];", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Order(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
