namespace WorkingWithData.Models;

public class Customer
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public int MembershipTypeId { get; set; }
}