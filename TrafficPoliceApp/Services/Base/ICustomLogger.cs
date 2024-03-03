using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Services.Base;

public interface ICustomLogger
{
    Task Log(Logging log);
    bool IsLoggingEnabled();
}