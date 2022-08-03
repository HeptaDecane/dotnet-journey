namespace Authy.Models;

public class User
{
    public static readonly string AuthName = "UserAuth";
    
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string Name { get; set; }

    public string Role { get; set; }

    public override string ToString() {
        return Username;
    }
}