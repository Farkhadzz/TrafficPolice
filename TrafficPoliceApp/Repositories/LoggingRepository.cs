namespace TrafficPoliceApp.Repositories;

using System.Data.SqlClient;
using Dapper;
using TrafficPoliceApp.Data;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;

public class LoggingRepository : ILoggerRepository
{
    private readonly MyDbContext dbContext;
    
    public LoggingRepository(MyDbContext dbContext) => this.dbContext = dbContext;

    public async Task AddLogAsync(Logging log)
    {
        await this.dbContext.Logs.AddAsync(log);

        await this.dbContext.SaveChangesAsync();
    }
}