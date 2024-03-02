namespace TrafficPoliceApp.Repositories;

using System.Data.SqlClient;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using TrafficPoliceApp.Dtos;
using Dapper;
using Turbo.az.Data;


public class UserRepository : IUserRepository
{
    private readonly string ConnectionString;
    private readonly MyDbContext dbContext;
    public UserRepository(MyDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public UserRepository(string ConnectionString)
    {
        this.ConnectionString = ConnectionString;
    }

    public IEnumerable<User> GetAllAsync()
    {
        var users = this.dbContext.Users.Where( user => 
        user.FirstName != "Admin".AsEnumerable());

        return users;
    }

    public async Task InsertUserAsync(User user) {
        this.dbContext.Users.Add(user);
        await this.dbContext.SaveChangesAsync();
    }

    public User GetUser(UserDto userDto)
    {
        var user = this.dbContext.Users.FirstOrDefault( u => 
        u.Email == userDto.Email && u.Password == userDto.Password);

        return user;
    }

    public async Task<User> GetUserById(User user)
    {
        var userId = await this.dbContext.Users.FindAsync(user.Id);

        return userId;
    }
}