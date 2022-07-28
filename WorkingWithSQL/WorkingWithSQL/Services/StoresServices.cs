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

    public async Task<int> CreateAsync(Store store)
    {
        string query = $"insert into Stores(Name, Address, Pin, Phone, Email)" +
                       $"values ('{store.Name}','{store.Address}','{store.Pin}','{store.Phone}','{store.Email}')";
        SqlCommand command = new SqlCommand(query, _connection);
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }
}