namespace WorkingWithData.Models;

// using MongoDB.Bson;
// using MongoDB.Bson.Serialization.Attributes;


public class MembershipType {
    public int Id { get; set; }
    
    public string Tier { get; set; }
    
    public double SignUpFee { get; set; }
    
    public int DurationInMonths { get; set; }

    public int DiscountPercentage { get; set; }
    
}