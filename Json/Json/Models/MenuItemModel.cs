using Newtonsoft.Json;
namespace Json.Models;

public class Batter
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}

public class Topping
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}

public class MenuItemModel
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("ppu")]
    public double Ppu { get; set; }

    [JsonProperty("batters")]
    public List<Batter> Batters { get; set; }

    [JsonProperty("toppings")]
    public List<Topping> Toppings { get; set; }
}