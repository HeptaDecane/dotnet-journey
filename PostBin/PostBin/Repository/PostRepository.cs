using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostBin.Models;
using PostBin.Shared;

namespace PostBin.Repository
{
    public interface IPostRepository : IRepository<Post>{}
    public class PostRepository : IPostRepository
    {
        private readonly AppSettings _appSettings;
        public PostRepository(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<DataTable> GetAsync()
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.get_posts, connection);
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
            }
            
            return dataTable;
        }

        public async Task<Post> GetAsync(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.get_post, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                
                if (await reader.ReadAsync())
                {
                    return new Post {
                        Id = reader.GetInt32(Post.Columns.Id),
                        UserId = reader.GetInt32(Post.Columns.UserId),
                        Title = reader.GetString(Post.Columns.Title),
                        Body = reader.GetString(Post.Columns.Body)
                    };
                }
            }
            
            return null;
        }

        public async Task CreateAsync(Post post)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.create_post, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Body", post.Body);
                command.Parameters.AddWithValue("@UserId", post.UserId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        
        public async Task UpdateAsync(int id, Post post)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.update_post, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", post.Title);
                command.Parameters.AddWithValue("@Body", post.Body);
                command.Parameters.AddWithValue("@Id", id);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
            
        }
        
        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(_appSettings.DefaultDb))
            {
                var command = new SqlCommand(StoredProcedures.delete_post, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
        
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        
    }
}