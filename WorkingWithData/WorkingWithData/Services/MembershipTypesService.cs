using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorkingWithData.Models;

namespace WorkingWithData.Services;

public class MembershipTypesService
{
    private readonly IMongoCollection<MembershipType> _collection;

    public MembershipTypesService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<MembershipType>("MembershipTypes");
    }

    public async Task<List<MembershipType>> GetAsync()
    {
        return await _collection.Find(x => true).ToListAsync();
    }

    public async Task<MembershipType> GetAsync(int id)
    {
        return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

}