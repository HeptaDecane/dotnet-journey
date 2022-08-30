using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostBin.Models;
using PostBin.Shared;

namespace PostBin.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByUsernameAsync(string username);
    }
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;

        public UserRepository(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }
        
        public async Task<User> GetAsync(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.get_user_by_username, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new User {
                        Id = reader.GetInt32(User.Columns.Id),
                        Username = reader.GetString(User.Columns.Username),
                        Password = reader.GetString(User.Columns.Password),
                        Salt = reader.GetString(User.Columns.Salt),
                        Name = reader.GetString(User.Columns.Name),
                        Role = reader.GetString(User.Columns.Role)
                    };
                }
            }

            return null;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.get_user_by_username, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new User {
                        Id = reader.GetInt32(User.Columns.Id),
                        Username = reader.GetString(User.Columns.Username),
                        Password = reader.GetString(User.Columns.Password),
                        Salt = reader.GetString(User.Columns.Salt),
                        Name = reader.GetString(User.Columns.Name),
                        Role = reader.GetString(User.Columns.Role)
                    };
                }
            }

            return null;
        }

        public async Task CreateAsync(User user)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.create_user, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Name", user.Name??"");
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Salt", user.Salt);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        
        public async Task UpdateAsync(int id, User user)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.update_user, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Salt", user.Salt);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            
        }

        public Task<DataTable> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}