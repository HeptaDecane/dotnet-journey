namespace WorkingWithData.Models;

using MongoDB.Bson.Serialization.Attributes;

public class Customer
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    
    [BsonElement("MembershipType")]
    public int MembershipTypeId { get; set; }
}