using WorkingWithData.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WorkingWithData.Services;

public class BooksService {
    private readonly IMongoCollection<Book> _collection;

    public BooksService(IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings) {
        var mongoClient = new MongoClient(bookStoreDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(bookStoreDatabaseSettings.Value.DatabaseName);
        _collection = mongoDatabase.GetCollection<Book>("Books");
    }

    public async Task<List<Book>> GetAsync() => await _collection.Find(_ => true).ToListAsync();

    public async Task<Book?> GetAsync(int id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Book newBook) => await _collection.InsertOneAsync(newBook);

    public async Task UpdateAsync(int id, Book updatedBook) => await _collection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task DeleteAsync(int id) => await _collection.DeleteOneAsync(x => x.Id == id);
}