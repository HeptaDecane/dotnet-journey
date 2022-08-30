using System.ComponentModel.DataAnnotations;

namespace PostBin.Models
{
    public class User
    {
        public static readonly string AuthName = "UserAuth";

        public static class Columns
        {
            public static readonly string Id = "Id";
            public static readonly string Username = "Username";
            public static readonly string Password = "Password";
            public static readonly string Salt = "Salt";
            public static readonly string Name = "Name";
            public static readonly string Role = "Role";
        }

        public static class Roles
        {
            public static readonly string User = "USER";
            public static readonly string Staff = "STAFF";
            public static readonly string Admin = "ADMIN";
        }
    
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }

        public override string ToString() {
            return Username;
        }
    }
}