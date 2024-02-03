using TrafficPoliceApp.Models;
using TrafficPoliceApp.Dtos;

namespace TrafficPoliceApp.Repositories.Base
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        // Task<bool> IsEmailUniqueAsync(string email);
        Task InsertUserAsync(User user);
        Task<User?> GetUser(UserDto userDto);
        Task<User?> GetUserById(User user);
    }
}