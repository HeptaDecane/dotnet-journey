namespace Authy.Models;

public class JwtResponse
{
    public static readonly string AuthName = "jwt";
    public string AccessToken { get; set; } = "";
    public DateTime ExpiresAt { get; set; }
}