using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PostBin.Repository
{
    public interface IRepository<T>
    {
        public Task<DataTable> GetAsync();
        public Task<T> GetAsync(int id);
        public Task CreateAsync(T entity);
        public Task UpdateAsync(int id, T entity);
        public Task DeleteAsync(int id);
    }
}