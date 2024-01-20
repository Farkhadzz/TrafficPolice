namespace TrafficPoliceApp.Repositories;

using System.Data.SqlClient;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using Dapper;

public class FineRepository : IFineRepository
{
    private const string ConnectionString = "Server=localhost;Database=TrafficPoliceDb;Trusted_Connection=True;";

    public async Task<IEnumerable<Fine>> GetAllAsync()
    {
        using var connection = new SqlConnection(ConnectionString);

        var fines = await connection.QueryAsync<Fine>("SELECT * FROM [Fines]");

        return fines;
    }

    public async Task InsertFineAsync(Fine fine)
    {
        using var connection = new SqlConnection(ConnectionString);

        var fines = await connection.ExecuteAsync(
            sql: "INSERT INTO [Fines] (FineName, Price) values (@FineName, @Price);",
            param: fine);
    }
}