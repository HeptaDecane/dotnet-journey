using System.Net.Http.Headers;
using Authy.Dtos;
using Authy.Models;
using Microsoft.Net.Http.Headers;

namespace Authy.Services;

public class WeatherApiServices
{
    private readonly HttpClient _http = new HttpClient();
    private readonly string _baseUrl = "https://localhost:44372";

    public async Task<List<WeatherForecastDto>> GetForecastsAsync(string accessToken)
    {
        var forecasts = new List<WeatherForecastDto>();
        try {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _http.GetAsync($"{_baseUrl}/WeatherForecast");
            response.EnsureSuccessStatusCode();
            forecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecastDto>>();
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }

        return forecasts;
    }

    public async Task<JwtResponse> Authenticate(Auth auth)
    {
        var jwtResponse = new JwtResponse();
        try
        {
            var response = await _http.PostAsJsonAsync($"{_baseUrl}/Auth", auth);
            response.EnsureSuccessStatusCode();
            jwtResponse = await response.Content.ReadFromJsonAsync<JwtResponse>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return jwtResponse;
    }
}