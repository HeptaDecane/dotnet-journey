using System;
using System.Security.Cryptography;
using System.Text;

namespace PostBin.Shared
{
    public static class Crypto
    {
        public static string GenerateSalt() {
            var random = new Random();
            string set = "abcdefghijklmnopqrstuvwxyz";
            string salt = "";

        
            for(int i=0; i<8; i++) {
                int idx = random.Next(set.Length);
                salt = salt + set[idx];
            }

            return salt;
        }
        public static string Hash(string password, string salt) {
        
            // Create a SHA256   
            var sha256 = SHA256.Create();
        
            // ComputeHash - returns byte array  
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password+salt));  
  
            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();  
            foreach (byte b in bytes) builder.Append(b.ToString("x2"));

            return builder.ToString();
        }
    }
}