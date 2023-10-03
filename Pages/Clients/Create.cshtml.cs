using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace MyStore.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public Clientinfo clientInfo = new Clientinfo();

        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {

        }

        public async void OnPost()
        {

            //Clientinfo = new Clientinfo();: This line creates a new instance


            //Clientinfo.name = Request.Form["name"];: This line retrieves the value of the
            //"name"
            //field from the HTTP POST request's form data using the
            //Request.Form collection and assigns it to the name
            //property of the Clientinfo object




            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];
            clientInfo.client_Type = Request.Form["client_Type"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0
                || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0 || clientInfo.client_Type.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }



            try
            {
                string connectionString = "Data Source=MY-PC;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open the connection
                    connection.Open();
                    string sql = "INSERT INTO clients " + " (name,email,phone,address,client_Type)VALUES " + " (@name,@email,@phone,@address,@client_Type); ";


                    using (SqlCommand command = new SqlCommand(sql, connection))

                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);
                        command.Parameters.AddWithValue("@client_type", clientInfo.client_Type);



                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // TODO: Implement database interaction to save the new client
            // Example using Entity Framework:
            // dbContext.Clients.Add(Clientinfo);
            // dbContext.SaveChanges();


            clientInfo.name = "";
            clientInfo.email = "";
            clientInfo.phone = "";
            clientInfo.address = "";
            clientInfo.client_Type = "";




            Response.Redirect("/Clients/Read");
            successMessage = "New client added successfully";
        }





       
    }
}               //try
                //{
                //    // Serialize the clientInfo object to JSON
                //    string json = JsonConvert.SerializeObject(clientInfo);

//    // Create a StringContent object with JSON data
//    var content = new StringContent(json, Encoding.UTF8, "application/json");

//    // URL of your API endpoint for saving the client
//    string apiUrl = "https://localhost:44328/api/Client/Alltheclients"; // Replace with your actual API URL

//    using (HttpClient client = new HttpClient())
//    {
//        // Send a POST request to the API endpoint with the JSON data
//        var response = await client.PostAsync(apiUrl, content);

//        if (response.IsSuccessStatusCode)
//        {
//            successMessage = "New client added successfully";
//        }
//        else
//        {
//            errorMessage = "Failed to add a new client. API returned status code: " + response.StatusCode;
//        }
//    }
//}
//catch (Exception ex)
//{
//    errorMessage = ex.Message;
//}

// Clear the form fields after successful submission (if needed)