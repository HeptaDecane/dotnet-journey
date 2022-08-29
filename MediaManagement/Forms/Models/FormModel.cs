namespace Forms.Models;

public class FormModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public IFormFile? Image { get; set; }

    public override string ToString()
    {
        return $"title: {Title ?? null}\n" +
               $"description: {Description ?? null}\n";
    }
}