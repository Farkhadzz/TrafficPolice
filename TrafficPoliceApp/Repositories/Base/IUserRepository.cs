using TrafficPoliceApp.Models;
using TrafficPoliceApp.Dtos;

namespace TrafficPoliceApp.Repositories.Base
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task InsertUserAsync(User user);
        Task<User?> GetUser(UserDto userDto);
    }
}