namespace BuildingForms.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }

    public override string ToString()
    {
        return $"Book:\n" +
               $"\tId: {Id}\n" +
               $"\tTitle: {Title}\n" +
               $"\tAuthor: {Author}\n" +
               $"\tCategory: {Category}\n" +
               $"\tStock: {Stock}";
    }
}