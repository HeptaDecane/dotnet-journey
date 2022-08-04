using Authy.Dtos;

namespace Authy.Services;

public class WeatherApiServices
{
    private readonly HttpClient _http = new HttpClient();
    private readonly string _baseUrl = "https://localhost:44372";

    public async Task<List<WeatherForecastDto>> GetForecastsAsync()
    {
        var forecasts = new List<WeatherForecastDto>();
        try {
            var response = await _http.GetAsync($"{_baseUrl}/WeatherForecast");
            response.EnsureSuccessStatusCode();
            forecasts = await response.Content.ReadFromJsonAsync<List<WeatherForecastDto>>();
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }

        return forecasts;
    }
}