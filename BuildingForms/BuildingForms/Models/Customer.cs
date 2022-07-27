namespace BuildingForms.Models;

public class Customer
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Subscribed { get; set; }

    public override string ToString()
    {
        return $"Customer:\n" +
               $"\tId: {Id}\n" +
               $"\tUsername: {Username}\n" +
               $"\tName: {Name}\n" +
               $"\tEmail: {Email}\n" +
               $"\tSubscribed: {Subscribed}";
    }
}