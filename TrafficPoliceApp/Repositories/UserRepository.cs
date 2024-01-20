namespace TrafficPoliceApp.Repositories;

using System.Data.SqlClient;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using Dapper;

public class UserRepository : IUserRepository
{
    private const string ConnectionString = "Server=localhost;Database=TrafficPoliceDb;Trusted_Connection=True;";

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = new SqlConnection(ConnectionString);

        var users = await connection.QueryAsync<User>("SELECT * FROM [Users]");

        return users;
    }

    public async Task InsertUserAsync(User user) {
        
        using var connection = new SqlConnection(ConnectionString);
        
        var users = await connection.ExecuteAsync(
            sql: "INSERT INTO [Users] (FirstName, LastName, Email) values (@FirstName, @LastName, @Email);",
            param: user);
    }
}