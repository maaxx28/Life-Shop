namespace LifeShop.Models
{
    public class Payment
    {
        private SqlConnection Connection = new(ConnectionStrings.local);

        public int ID { get; private set; }
        public int CustomerID { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public int CVC { get; set; }
        public string PaymentAddress { get; set; }
        public string PaymentCity { get; set; }
        public string PaymentState { get; set; }
        public string PaymentZip { get; set; }
        public Payment(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM Payment WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                CustomerID = theReader.GetInt32(1);
                CardType = theReader.GetString(2);
                CardNumber = theReader.GetString(3);
                CVC = theReader.GetInt32(4);
                PaymentAddress = theReader.GetString(5);
                PaymentCity = theReader.GetString(6);
                PaymentState = theReader.GetString(7);
                PaymentZip = theReader.GetString(8);

                Connection.Close();
            }
            else
            {
                CustomerID = 0;
                CardType = "";
                CardNumber = "";
                CVC = 0;
                PaymentAddress = "";
                PaymentCity = "";
                PaymentState = "";
                PaymentZip = "";
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("PaymentUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
            theCommand.Parameters.AddWithValue("@CardType", CardType);
            theCommand.Parameters.AddWithValue("@CardNumber", CardNumber);
            theCommand.Parameters.AddWithValue("CVC", CVC);
            theCommand.Parameters.AddWithValue("@BillAddress", PaymentAddress);
            theCommand.Parameters.AddWithValue("@BillCity", PaymentCity);
            theCommand.Parameters.AddWithValue("@BillState", PaymentState);
            theCommand.Parameters.AddWithValue("@BillZip", PaymentZip);


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
            SqlCommand theCommand = new("DELETE FROM Payment WHERE ID=" + id + ";", staticConnection);
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
        public static List<Payment> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Payment> list = new();
            SqlCommand theCommand = new("SELECT ID From Payment;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Payment(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
