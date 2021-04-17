using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SpaClient.Models;

namespace SpaClient.ApiClient
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<List<WeatherForecast>> GetWeatherForecastsAsync()
        {
            var response = await _httpClient.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
        }
    }
}
