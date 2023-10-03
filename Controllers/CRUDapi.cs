using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDapi : ControllerBase
    {
        private string connectionString = "Data Source=MY-PC;Initial Catalog=mystore;Integrated Security=True";

        public class Client
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }

            public string Phone { get; set; }

            public string Address { get; set; }

            public string Client_Type { get; set; }
        }


        //TO RETREIVE THE ALL CLIENTS FROM THE DATABASE

        [HttpGet("Alltheclients")]
        public IActionResult GetAllClients()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM clients";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<Client> clients = new List<Client>();

                            while (reader.Read())
                            {
                                Client client = new Client
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    Address = reader.GetString(4),
                                    Client_Type = reader.GetString(5)
                                };

                                clients.Add(client);
                            }

                            return Ok(clients);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //TO RETRIEVE THE CLIENT USING ID

        [HttpGet("Retreive/{id}")]
        public IActionResult GetClientById(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "SELECT * FROM clients WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Client client = new Client
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    Phone = reader.GetString(3),
                                    Address = reader.GetString(4),
                                    Client_Type = reader.GetString(5)
                                };

                                return Ok(client);
                            }
                            else
                            {
                                return NotFound("Client not found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        //TO CREATE A NEW CLIENT

        [HttpPost("Newclient")]
        public IActionResult CreateClient([FromBody] Client client)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "INSERT INTO clients (name, email, phone, address, client_Type) VALUES (@name, @email, @phone, @address, @client_Type)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@name", client.Name);
                        cmd.Parameters.AddWithValue("@email", client.Email);
                        cmd.Parameters.AddWithValue("@phone", client.Phone);
                        cmd.Parameters.AddWithValue("@address", client.Address);
                        cmd.Parameters.AddWithValue("@client_Type", client.Client_Type);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Client created successfully.");
                        }
                        else
                        {
                            return BadRequest("Failed to create client.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //TO UPDATE THE CLIENT INFORMATION USING THEIR ID


        [HttpPut("update/{id}")]
        public IActionResult UpdateClient(int id, [FromBody] Client client)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "UPDATE clients SET name = @name, email = @email, phone = @phone, address = @address, client_Type = @client_Type WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", client.Name);
                        cmd.Parameters.AddWithValue("@email", client.Email);
                        cmd.Parameters.AddWithValue("@phone", client.Phone);
                        cmd.Parameters.AddWithValue("@address", client.Address);
                        cmd.Parameters.AddWithValue("@client_Type", client.Client_Type);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Client updated successfully.");
                        }
                        else
                        {
                            return NotFound("Client not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //TO DELETE THE CLIENT USIND ID

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "DELETE FROM clients WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Client deleted successfully.");
                        }
                        else
                        {
                            return NotFound("Client not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

