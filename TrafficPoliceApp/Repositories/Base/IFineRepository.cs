using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Repositories.Base
{
    public interface IFineRepository
    {
        Task<IEnumerable<Fine>> GetAllAsync();
        Task InsertFineAsync(Fine fine);
    }
}