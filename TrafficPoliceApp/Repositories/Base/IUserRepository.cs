using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Repositories.Base
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task InsertUserAsync(User user);
    }
}