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

    public async Task InsertFineForUserAsync(Fine fine, int userId)
    {
        using var connection = new SqlConnection(ConnectionString);

        await connection.ExecuteAsync(
            "INSERT INTO [Fines] (FineName, CarNumber, CarModel, Price, UserId) VALUES (@FineName, @CarNumber, @CarModel, @Price, @UserId)",
            new { fine.FineName, fine.CarNumber, fine.CarModel, fine.Price, UserId = userId });
    }


    // public async Task InsertFineAsync(Fine fine)
    // {
    //     using var connection = new SqlConnection(ConnectionString);

    //     var fines = await connection.ExecuteAsync(
    //         sql: "INSERT INTO [Fines] (FineName, Price) values (@FineName, @Price);",
    //         param: fine);
    // }

    public async Task<IEnumerable<string>> GetColumnsAsync()
    {
        using var connection = new SqlConnection(ConnectionString);
        var columns = await connection.QueryAsync<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Fines'");
        return columns;
    }
}