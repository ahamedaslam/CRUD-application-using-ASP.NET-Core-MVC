using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{ 
    public class UpdateModel : PageModel
    {
        public Clientinfo clientinfo = new Clientinfo();
        public string errorMessage = "";
        public string successMessage = "";
        
        
        // This method allows us to see the data of the current client
        public void OnGet()
        {
            String id = Request.Query["id"]; //To read the id of the employee


            try
            {
                String connectionString = "Data Source=MY-PC;Initial Catalog=mystore;Integrated Security=True";

                //To create a sql connection object

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    //The Open() method is called on the
                    //sqlConnection to establish a connection
                    //to the database.

                    //QUERY ALL THE ROWS FROM THE TABLE
                    string sql = "SELECT * FROM clients WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))


                    {
                        //A SqlDataReader object (reader) is created by calling
                        //ExecuteReader() on the command. The SqlDataReader is used
                        //to read and access the data returned by the
                        //SQL query.

                        command.Parameters.AddWithValue("@id",id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                           
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);
                                clientinfo.client_Type = reader.GetString(5);
                                clientinfo.created_at = reader.GetDateTime(6).ToString();
                               
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;

            }
        }
        // To update the data of the clients 
        public void OnPost()
        {
            clientinfo.id = Request.Form["id"];
            clientinfo.name = Request.Form["name"];
            clientinfo.email = Request.Form["email"];
            clientinfo.phone = Request.Form["phone"];
            clientinfo.address = Request.Form["address"];
            clientinfo.client_Type = Request.Form["client_Type"];



            if (clientinfo.id.Length == 0 || clientinfo.name.Length == 0 || clientinfo.email.Length == 0
                || clientinfo.phone.Length == 0 || clientinfo.address.Length == 0 || 
                clientinfo.client_Type.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

             //To store the updated values in the database
            try
            {
                string connectionString = "Data Source=MY-PC;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open the connection
                    connection.Open();
                    string sql = "UPDATE clients " +
                                 "SET name=@name, email=@email, phone=@phone, address=@address, client_Type=@client_Type " +
                                 "WHERE id=@id";



                    using (SqlCommand command = new SqlCommand(sql, connection))

                    {
                        command.Parameters.AddWithValue("@id", clientinfo.id);
                        command.Parameters.AddWithValue("@name", clientinfo.name);
                        command.Parameters.AddWithValue("@email", clientinfo.email);
                        command.Parameters.AddWithValue("@phone", clientinfo.phone);
                        command.Parameters.AddWithValue("@address", clientinfo.address);
                        command.Parameters.AddWithValue("@client_Type", clientinfo.client_Type);


                        command.ExecuteNonQuery();
                    }
                }
            }


            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            //Redirect to the main page
           
            Response.Redirect("/Clients/Read");

        } 
    }

       
    
}
