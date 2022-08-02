using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClientSideDev.Models;

public class Item
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
    
    [BsonElement("completed")]
    public bool IsCompleted { get; set; }
    
    
    public override string ToString()
    {
        return $"Item:\n" +
               $"\tId: {Id}\n" +
               $"\tTitle: {Title}\n" +
               $"\tDescription: {Description}\n" +
               $"\tIsCompleted: {IsCompleted}";
    }
}