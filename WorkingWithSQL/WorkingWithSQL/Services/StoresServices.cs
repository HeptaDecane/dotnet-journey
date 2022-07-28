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

    public List<Store> Get()
    {
        SqlCommand command = new SqlCommand("select * from Stores", _connection);
        command.CommandType = CommandType.Text;
        
        _connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        List<Store> stores = new List<Store>();
        while (reader.Read()) {
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

}