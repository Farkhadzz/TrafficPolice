using TrafficPoliceApp.Models;
using TrafficPoliceApp.Dtos;

namespace TrafficPoliceApp.Repositories.Base
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllAsync();
        Task InsertUserAsync(User user);
        User GetUser(UserDto userDto);
        Task<User?> GetUserById(User user);
    }
}