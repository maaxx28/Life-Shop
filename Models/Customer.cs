namespace LifeShop.Models
{
    public class Customer
    {
        private SqlConnection Connection = new(ConnectionStrings.local);

        public int ID { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Customer(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM Customer WHERE ID = " + id, Connection);
                SqlDataReader theReader = theCommand.ExecuteReader();
                theReader.Read();

                FirstName = theReader.GetString(1);
                LastName = theReader.GetString(2);
                Email = theReader.GetString(3);
                Phone = theReader.GetInt32(4);
                Address = theReader.GetString(5);
                City = theReader.GetString(6);
                State = theReader.GetString(7);
                Zip = theReader.GetInt32(8);
                UserName = theReader.GetString(9);
                Password = theReader.GetString(10);
            }
            else
            {
                FirstName = "";
                LastName = "";
                Email = "";
                Phone = 0000000000;
                Address = "";
                City = "";
                State = "";
                Zip = 00000;
                UserName = "";
                Password = "";
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("CustomerUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@CustomerFirstName", FirstName);
            theCommand.Parameters.AddWithValue("@CustomerLastName", LastName);
            theCommand.Parameters.AddWithValue("@CustomerEmail", Email);
            theCommand.Parameters.AddWithValue("@CustomerPhone", Phone);
            theCommand.Parameters.AddWithValue("@CustomerAddress", Address);
            theCommand.Parameters.AddWithValue("@CustomerCity", City);
            theCommand.Parameters.AddWithValue("@CustomerState", State);
            theCommand.Parameters.AddWithValue("@CustomerZip", Zip);
            theCommand.Parameters.AddWithValue("@CustomerUsername", UserName);
            theCommand.Parameters.AddWithValue("@CustomerPassword", Password);

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
            SqlCommand theCommand = new("DELETE FROM Employee WHERE ID=" + id + ";", staticConnection);
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
        public static List<Customer> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Customer> list = new();
            SqlCommand theCommand = new("SELECT ID From Customer;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Customer(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }

        public static int ValidateLogin(string userName, string password)
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            SqlCommand theCommand = new("SELECT ID FROM Customer WHERE Username='" + userName + "' AND Password='" + password + "';", staticConnection);
            staticConnection.Open();
            int theID;
            try
            {
                theID = Convert.ToInt32(theCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                theID = 0;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                staticConnection.Close();
            }
            return theID;
        }
    }
}
