using System.Text;
using Json.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Json.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
{
    private readonly string _baseUrl = "http://localhost:3000";
    private readonly HttpClient _http = new HttpClient();
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _http.GetAsync($"{_baseUrl}/displayCards");
        var cards = await response.Content.ReadFromJsonAsync<List<DisplayCardModel>>();
        
        response = await _http.GetAsync($"{_baseUrl}/displayCards");
        // plain json string
        var jsonString = await response.Content.ReadAsStringAsync();

        response = await _http.GetAsync($"{_baseUrl}/displayCards/1");
        // cannot access (dot)prop
        var jsonElement = await response.Content.ReadFromJsonAsync<dynamic>();
        
        response = await _http.GetAsync($"{_baseUrl}/displayCards/1");
        var card = await response.Content.ReadFromJsonAsync<DisplayCardModel>();
        jsonString = JsonConvert.SerializeObject(card);
        // can access (dot)prop
        var jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

        response = await _http.GetAsync($"{_baseUrl}/displayCards");
        jsonString = await response.Content.ReadAsStringAsync();
        // can access (dot)prop
        var jsonArray = JsonConvert.DeserializeObject<dynamic>(jsonString);
        
        // posting object
        response = await _http.GetAsync($"{_baseUrl}/displayCards/1");
        card = await response.Content.ReadFromJsonAsync<DisplayCardModel>();
        card.Id = "2";
        response = await _http.PostAsJsonAsync($"{_baseUrl}/displayCards", card);
        jsonString = await response.Content.ReadAsStringAsync();
        
        // posting json string
        response = await _http.GetAsync($"{_baseUrl}/displayCards/1");
        jsonString = await response.Content.ReadAsStringAsync();
        jsonString = jsonString.Replace("1", "3");
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
        response = await _http.PostAsync($"{_baseUrl}/displayCards", content);
        jsonString = await response.Content.ReadAsStringAsync();

        return Ok();
    }
}