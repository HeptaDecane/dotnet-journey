using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using WeatherApi.Models;

namespace WeatherApi.Services;

public class UsersServices
{
    private readonly SqlConnection _connection;

    public UsersServices(IOptions<DatabaseSettings> options) {
        _connection = new SqlConnection(options.Value.ConnectionString);
    }
    public async Task<List<User>> GetAsync() {
        
        List<User> users = new List<User>();
        string query = "select * from Users";
        var command = new SqlCommand(query, _connection);
        
        try {
            await _connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) {
                
                users.Add(new User() {
                    Id = (int)reader["Id"],
                    Username = (string)reader["Username"],
                    Name = (string)reader["Name"],
                    Password = (string)reader["Password"],
                    Salt = (string)reader["Salt"],
                    Role = (string)reader["Role"]
                });
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }
        finally {
            _connection.Close();
        }

        return users;
    }

    public async Task<User> GetAsync(int id) {
        string query = $"select * from Users where Id={id}";
        var command = new SqlCommand(query, _connection);

        User user = new User();
        try {
            await _connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) {
                user.Id = (int)reader["Id"];
                user.Username = (string)reader["Username"];
                user.Name = (string)reader["Name"];
                user.Password = (string)reader["Password"];
                user.Salt = (string)reader["Salt"];
                user.Role = (string)reader["Role"];
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }
        finally {
            _connection.Close();
        }

        return user;
    }

    public async Task<User> GetAsync(string username) {
        string query = $"select * from Users where Username=@Username";
        var command = new SqlCommand(query, _connection);
        command.Parameters.AddWithValue("@Username", username);
        
        User user = new User();
        try {
            await _connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync()) {
                user.Id = (int)reader["Id"];
                user.Username = (string)reader["Username"];
                user.Name = (string)reader["Name"];
                user.Password = (string)reader["Password"];
                user.Salt = (string)reader["Salt"];
                user.Role = (string)reader["Role"];
            }
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }
        finally {
            _connection.Close();
        }

        return user;
    }

    public async Task<int> CreateAsync(User user) {
        string query = "insert into Users (Username,Password,Salt,Name)\n" +
                       $"values (@Username,@Password,@Salt,@Name)";
        var command = new SqlCommand(query, _connection);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.Password);
        command.Parameters.AddWithValue("@Salt", user.Salt);
        command.Parameters.AddWithValue("@Name", user.Name);
        
        int rowsAffected = 0;

        try {
            await _connection.OpenAsync();
            rowsAffected = await command.ExecuteNonQueryAsync();
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }
        finally {
            _connection.Close();
        }

        return rowsAffected;
    }

    public async Task<int> UpdateAsync(int id, User user) {
        string query = "update Users set\n" +
                       "Username=@Username, Password=@Password, Salt=@Salt, Name=@Name, Role=@Role\n" +
                       "where Id=@Id";
        var command = new SqlCommand(query, _connection);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.Password);
        command.Parameters.AddWithValue("@Salt", user.Salt);
        command.Parameters.AddWithValue("@Name", user.Name);
        command.Parameters.AddWithValue("@Role", user.Role);
        command.Parameters.AddWithValue("@Id", id);

        int rowsAffected = 0;
        try {
            await _connection.OpenAsync();
            rowsAffected = await command.ExecuteNonQueryAsync();
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }
        finally {
            _connection.Close();
        }

        return rowsAffected;
    }

    public async Task<int> DeleteAsync(int id) {
        string query = $"delete from Users where Id={id}";
        var command = new SqlCommand(query, _connection);
        
        int rowsAffected = 0;
        try {
            await _connection.OpenAsync();
            rowsAffected = await command.ExecuteNonQueryAsync();
        }
        catch (Exception e) {
            Console.WriteLine(e);
        }
        finally {
            _connection.Close();
        }

        return rowsAffected;
    }
}