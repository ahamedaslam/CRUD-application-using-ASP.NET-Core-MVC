using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(string apiUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(apiUrl)
        };

        // Optionally, set default request headers (e.g., for authentication, if required)
        // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_ACCESS_TOKEN");
    }

    // Method to make GET requests to the API
    public async Task<string> GetAsync(string endpoint)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Method to make POST requests to the API
    public async Task<string> PostAsync(string endpoint, string jsonData)
    {
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Method to make PUT requests to the API
    public async Task<string> PutAsync(string endpoint, string jsonData)
    {
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync(endpoint, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    // Method to make DELETE requests to the API
    public async Task<string> DeleteAsync(string endpoint)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
