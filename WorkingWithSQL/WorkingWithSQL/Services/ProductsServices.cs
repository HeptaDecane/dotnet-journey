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
        var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(new Product()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                Category = (string)reader["Category"],
                ImageUrl = (string)reader["Image"]
            });
        }
        _connection.Close();

        return products;
    }

    public async Task<int> CreateAsync(Product product)
    {
        string query = $"insert into Products(Name, Description, Category, Image)" +
                       $"values ('{product.Name}','{product.Description}','{product.Category}','{product.ImageUrl}')";
        SqlCommand command = new SqlCommand(query, _connection);
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }
}