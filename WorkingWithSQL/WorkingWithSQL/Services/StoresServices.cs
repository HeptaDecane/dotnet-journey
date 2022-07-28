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
        SqlCommand command = new SqlCommand("select * from Stores", _connection);
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
        string query = $"select * from Stores where Id={id}";
        SqlCommand command = new SqlCommand(query, _connection);
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
        string query = $"insert into Stores(Name, Address, Pin, Phone, Email)\n" +
                       $"values ('{store.Name}','{store.Address}','{store.Pin}','{store.Phone}','{store.Email}')";
        SqlCommand command = new SqlCommand(query, _connection);
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }

    public async Task<int> UpdateAsync(Store store)
    {
        string query = $"update Stores set\n" +
                       $"Name = '{store.Name}',\n" +
                       $"Address = '{store.Address}',\n" +
                       $"Pin = '{store.Pin}',\n" +
                       $"Phone = '{store.Phone}',\n" +
                       $"Email = '{store.Email}'\n" +
                       $"where Id={store.Id}";
        var command = new SqlCommand(query, _connection);
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }

    public async Task<int> DeleteAsync(int id)
    {
        string query = $"delete from Stores where Id={id}";
        var command = new SqlCommand(query, _connection);
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }
}