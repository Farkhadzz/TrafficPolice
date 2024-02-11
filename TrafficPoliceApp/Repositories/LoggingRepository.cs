namespace TrafficPoliceApp.Repositories;

using System.Data.SqlClient;
using Dapper;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;

public class LoggingRepository : ILoggerRepository
{
    private readonly string connectionString;
    private readonly bool isLoggingEnabled;

    public LoggingRepository(string connectionString, bool isLoggingEnabled)
    {
        this.connectionString = connectionString;
        this.isLoggingEnabled = isLoggingEnabled;
    }

    public async Task Logging(Logging log)
    {
        using var connection = new SqlConnection(connectionString);

        await connection.ExecuteAsync(
            sql: "INSERT INTO Logs (UserId, Url, MethodType, StatusCode, RequestBody, ResponseBody) VALUES (@UserId, @Url, @MethodType, @StatusCode, @RequestBody, @ResponseBody);",
            param: log);
    }

    public bool IsLoggingEnabled() => isLoggingEnabled;
}