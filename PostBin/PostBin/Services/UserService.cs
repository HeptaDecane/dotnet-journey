using System.Threading.Tasks;
using PostBin.Models;
using PostBin.Repository;

namespace PostBin.Services
{
    public interface IUserService
    {
        public Task<User> GetByIdAsync(int id);
        public Task<User> GetByUsernameAsync(string username);
        public Task CreateAsync(User user);
        public Task<User> UpdateAsync(int id, User user);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetAsync(id);
        }
        
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task CreateAsync(User user)
        {
            await _userRepository.CreateAsync(user);
        }
        
        public async Task<User> UpdateAsync(int id, User user)
        {
            await _userRepository.UpdateAsync(id, user);
            return await _userRepository.GetAsync(id);
        }
    }
}