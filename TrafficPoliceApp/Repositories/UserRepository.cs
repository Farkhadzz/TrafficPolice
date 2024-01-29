namespace TrafficPoliceApp.Repositories;

using System.Data.SqlClient;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using TrafficPoliceApp.Dtos;
using Dapper;

public class UserRepository : IUserRepository
{
    private readonly string ConnectionString;

    public UserRepository(string ConnectionString)
    {
        this.ConnectionString = ConnectionString;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        using var connection = new SqlConnection(ConnectionString);

        var users = await connection.QueryAsync<User>("SELECT * FROM [Users]");

        return users;
    }

    public async Task InsertUserAsync(User user) {
        
        using var connection = new SqlConnection(ConnectionString);
        
        var users = await connection.ExecuteAsync(
            sql: "INSERT INTO [Users] (FirstName, LastName, Email, Age, Password) values (@FirstName, @LastName, @Email, @Age, @Password);",
            param: user);
    }

    public async Task<User> GetUser(UserDto userDto)
    {
        using var connection = new SqlConnection(ConnectionString);
        
        var user = await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email AND Password = @Password",
            new { Email = userDto.Email, Password = userDto.Password });

        return user;
    }

    // public async Task<bool> IsEmailUniqueAsync(string email)
    // {
    //     using var connection = new SqlConnection(ConnectionString);
    //     var result = await connection.ExecuteScalarAsync<bool>(
    //         "SELECT COUNT(*) FROM Users WHERE Email = @Email",
    //         new { Email = email });

    //     return result == null;
    // }
}