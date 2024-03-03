using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using TrafficPoliceApp.Services.Base;

namespace TrafficPoliceApp.Services;

public class CustomLogger : ICustomLogger
{
    private bool isLoggingEnabled;
    private readonly IConfiguration configuration;
    private readonly ILoggerRepository logRepository;

    public CustomLogger(IConfiguration configuration, ILoggerRepository logRepository)
    {
        this.configuration = configuration;
        
        this.logRepository = logRepository;

        SetIsLoggerEnabled();
    }

    public async Task Log(Logging log) => await logRepository.AddLogAsync(log);

    public bool IsLoggingEnabled() => isLoggingEnabled;

    private void SetIsLoggerEnabled() => isLoggingEnabled = configuration.GetSection("isCustomLoggingEnabled").Get<bool>();
}