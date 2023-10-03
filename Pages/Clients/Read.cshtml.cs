using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class ReadModel : PageModel
    {
        ////TO STORE ALL THE CLIENTS IN THIS LIST(ARRAYS)

        public List<Clientinfo> listClients = new List<Clientinfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=MY-PC;Initial Catalog=mystore;Integrated Security=True";

                //To create a sql connection object

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    //The Open() method is called on the
                    //sqlConnection to establish a connection
                    //to the database.

                    //QUERY ALL THE ROWS FROM THE TABLE
                    
                    string sql = "SELECT * FROM clients";

                    using (SqlCommand command = new SqlCommand(sql, sqlConnection))


                    {
                        //A SqlDataReader object (reader) is created by calling
                        //ExecuteReader() on the command. The SqlDataReader is used
                        //to read and access the data returned by the
                        //SQL query.
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Clientinfo clientinfo = new Clientinfo();
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = reader.GetString(1);
                                clientinfo.email = reader.GetString(2);
                                clientinfo.phone = reader.GetString(3);
                                clientinfo.address = reader.GetString(4);
                                clientinfo.client_Type = reader.GetString(5);
                                clientinfo.created_at = reader.GetDateTime(6).ToString();
                       
                                listClients.Add(clientinfo);

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());

            }
        }
    }


    public class Clientinfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string client_Type;
        public string created_at;
        

    }
}
