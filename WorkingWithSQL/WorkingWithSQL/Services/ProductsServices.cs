using System.Data;
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
        SqlCommand command = new SqlCommand("GetProducts", _connection);
        command.CommandType = CommandType.StoredProcedure;
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

    public async Task<Product> GetAsync(int id)
    {
        Product product = new Product();
        var command = new SqlCommand("GetProductById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);
        await _connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            product.Id = (int)reader["Id"];
            product.Name = (string)reader["Name"];
            product.Category = (string)reader["Category"];
            product.Description = (string)reader["Description"];
            product.ImageUrl = (string)reader["Image"];
        }
        _connection.Close();

        return product;
    }
    public async Task<int> CreateAsync(Product product)
    {
        SqlCommand command = new SqlCommand("CreateProduct", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Category", product.Category);
        command.Parameters.AddWithValue("@Image", product.ImageUrl);
        
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }

    public async Task<int> UpdateAsync(Product product)
    {
        var command = new SqlCommand("UpdateProductById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", product.Id);
        command.Parameters.AddWithValue("@Name", product.Name);
        command.Parameters.AddWithValue("@Description", product.Description);
        command.Parameters.AddWithValue("@Category", product.Category);
        command.Parameters.AddWithValue("@Image", product.ImageUrl);
        
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var command = new SqlCommand("DeleteProductById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Id", id);
        
        await _connection.OpenAsync();
        int rowsAffected = await command.ExecuteNonQueryAsync();
        _connection.Close();
        return rowsAffected;
    }
}