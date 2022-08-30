using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PostBin.Models
{
    public class Post
    {
        public static class Columns
        {
            public static readonly string Id = "Id";
            public static readonly string UserId = "UserId";
            public static readonly string Title = "Title";
            public static readonly string Body = "Body";
        }
        
        public int Id { get; set; }
        public int UserId { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Body { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}