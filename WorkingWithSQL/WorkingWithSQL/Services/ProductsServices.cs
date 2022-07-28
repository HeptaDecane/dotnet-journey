using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using WorkingWithSQL.Models;

namespace WorkingWithSQL.Services;

public class ProductsServices
{
    private readonly SqlConnection _connection;

    public ProductsServices(IOptions<DatabaseConfigs> options)
    {
        _connection = new SqlConnection(options.Value.ConnectionString);
    }

    public async Task<List<Product>> GetAsync()
    {
        List<Product> products = new List<Product>();
        SqlCommand command = new SqlCommand("select * from Products", _connection);
        await _connection.OpenAsync();
        SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(new Product()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                Category = reader["Category"].ToString(),
                ImageUrl = reader["Image"].ToString()
            });
        }
        _connection.Close();

        return products;
    }
}