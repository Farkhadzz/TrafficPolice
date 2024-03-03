using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Repositories.Base;
public interface ILoggerRepository
{
    Task AddLogAsync(Logging log);
}