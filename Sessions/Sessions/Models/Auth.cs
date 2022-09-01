using System.ComponentModel.DataAnnotations;

namespace Sessions.Models
{
    public class Auth
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}