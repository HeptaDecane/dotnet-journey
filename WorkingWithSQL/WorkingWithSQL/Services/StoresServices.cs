using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using WorkingWithSQL.Models;

namespace WorkingWithSQL.Services;

public class StoresServices
{
    private readonly SqlConnection _connection;

    public StoresServices(IOptions<DatabaseConfigs> options)
    {
        _connection = new SqlConnection(options.Value.ConnectionString);
    }

    public async Task<List<Store>> GetAsync()
    {
        List<Store> stores = new List<Store>();
        SqlCommand command = new SqlCommand("GetStores", _connection);
        command.CommandType = CommandType.StoredProcedure;
        await _connection.OpenAsync();
        SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync()) {
            stores.Add(new Store() {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Address = reader["Address"].ToString(),
                Pin = reader["Pin"].ToString(),
                Email = reader["Email"].ToString(),
                Phone = reader["Phone"].ToString()
            });
        }
        _connection.Close();
        
        return stores;
    }

    public async Task<Store> GetAsync(int id)
    {
        Store store = new Store();
        SqlCommand command = new SqlCommand("GetStoreById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);
        await _connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync()) {
            store.Id = (int)reader["Id"];
            store.Name = (string)reader["Name"];
            store.Address = (string)reader["Address"];
            store.Pin = (string)reader["Pin"];
            store.Phone = (string)reader["Phone"];
            store.Email = (string)reader["Email"];
        }
        _connection.Close();

        return store;
    }

    public async Task<int> CreateAsync(Store store)
    {
        SqlCommand command = new SqlCommand("CreateStore", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Name", store.Name);
        command.Parameters.AddWithValue("@Address", store.Address);
        command.Parameters.AddWithValue("@Pin", store.Pin);
        command.Parameters.AddWithValue("@Phone", store.Phone);
        command.Parameters.AddWithValue("@Email", store.Email);
        
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }

    public async Task<int> UpdateAsync(Store store)
    {
        var command = new SqlCommand("UpdateStoreById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", store.Id);
        command.Parameters.AddWithValue("@Name", store.Name);
        command.Parameters.AddWithValue("@Address", store.Address);
        command.Parameters.AddWithValue("@Pin", store.Pin);
        command.Parameters.AddWithValue("@Phone", store.Phone);
        command.Parameters.AddWithValue("@Email", store.Email);
        
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var command = new SqlCommand("DeleteStoreById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);
        
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }
}