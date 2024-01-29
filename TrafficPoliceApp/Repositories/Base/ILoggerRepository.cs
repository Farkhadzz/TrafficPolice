using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Repositories.Base;
public interface ILoggerRepository
{
    Task Logging(Logging logging);
    bool IsLoggingEnabled();
}