namespace LifeShop.Models
{
    public class Employee
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
        public string Role { get; set; }

        public Employee(int id)
        {
            if (id != 0)
            {
                ID = id;
                Connection.Open();
                SqlCommand theCommand = new("SELECT * FROM Employee WHERE ID = " + id, Connection);
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
                Role = theReader.GetString(11);
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
                Role = "";
            }
        }
        public string Save()
        {
            SqlCommand theCommand = new("EmployeeUpdate", Connection);
            theCommand.CommandType = System.Data.CommandType.StoredProcedure;
            theCommand.Parameters.AddWithValue("@ID", ID);
            theCommand.Parameters.AddWithValue("@EmployeeFirstName", FirstName);
            theCommand.Parameters.AddWithValue("@EmployeeLastName", LastName);
            theCommand.Parameters.AddWithValue("@EmployeeEmail", Email);
            theCommand.Parameters.AddWithValue("@EmployeePhone", Phone);
            theCommand.Parameters.AddWithValue("@EmployeeAddress", Address);
            theCommand.Parameters.AddWithValue("@EmployeeCity", City);
            theCommand.Parameters.AddWithValue("@EmployeeState", State);
            theCommand.Parameters.AddWithValue("@EmployeeZip", Zip);
            theCommand.Parameters.AddWithValue("@EmployeeRole", Role);
            theCommand.Parameters.AddWithValue("@EmployeeUsername", UserName);
            theCommand.Parameters.AddWithValue("@EmployeePassword", Password);

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
        public static List<Employee> GetList()
        {
            SqlConnection staticConnection = new(ConnectionStrings.local);
            List<Employee> list = new();
            SqlCommand theCommand = new("SELECT ID From Employee;", staticConnection);
            staticConnection.Open();
            SqlDataReader theReader = theCommand.ExecuteReader();
            while (theReader.Read())
            {
                list.Add(new Employee(theReader.GetInt32(0)));
            }
            staticConnection.Close();
            return list;
        }
    }
}
