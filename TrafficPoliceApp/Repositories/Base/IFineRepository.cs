using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Repositories.Base
{
    public interface IFineRepository
    {
        Task<IEnumerable<Fine>> GetAllAsync();
        Task InsertFineForUserAsync(Fine fine, int userId);
        Task<IEnumerable<string>> GetColumnsAsync();
    }
}