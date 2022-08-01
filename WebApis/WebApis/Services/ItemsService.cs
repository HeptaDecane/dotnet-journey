using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApis.Models;

namespace WebApis.Services;

public class ItemsService
{
    private readonly IMongoCollection<Item> _collection;

    public ItemsService(IOptions<DatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<Item>("Items");
    }

    public async Task<List<Item>> GetAsync()
    {
        return await _collection.Find(x => true).ToListAsync();
    }

    public async Task<Item> GetAsync(string id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Item item)
    {
        await _collection.InsertOneAsync(item);
    }

    public async Task UpdateAsync(string id, Item item)
    {
        await _collection.ReplaceOneAsync(x => x.Id==id, item);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id==id);
    }

}