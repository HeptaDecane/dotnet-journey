using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorkingWithData.Models;

namespace WorkingWithData.Services;

public class CustomersService
{
    private readonly IMongoCollection<Customer> _collection;

    public CustomersService(IOptions<BookStoreDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<Customer>("Customers");
    }

    public async Task<List<Customer>> GetAsync()
    {
        return await _collection.Find(x => true).ToListAsync();
    }

    public async Task<Customer> GetAsync(int id)
    {
        return await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }

    public async Task CreateAsync(Customer customer)
    {
        await _collection.InsertOneAsync(customer);
    }

    public async Task UpdateAsync(int id, Customer customer)
    {
        await _collection.ReplaceOneAsync(x => x.Id == id, customer);
    }

    public async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }
}