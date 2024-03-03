using Microsoft.AspNetCore.Identity;
using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Services.Base;

namespace TrafficPoliceApp.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<User> signInManager;

    public IdentityService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.signInManager = signInManager;
    }

    public async Task<User?> FindByUserNameAsync(string userName)
    {
        var user = await this.userManager.FindByNameAsync(userName);

        return user;
    }

    public async Task AddRoleToUserAsync(User user, string roleType)
    {
        var role = new IdentityRole { Name = roleType };

        await roleManager.CreateAsync(role);

        await userManager.AddToRoleAsync(user, role.Name);
    }

    public async Task<IdentityResult> RegisterAsync(UserDto userDto)
    {
        var newUser = new User
        {
            Email = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };

        var result = await this.userManager.CreateAsync(newUser, userDto.Password!);
        if(result is null)
        {
            System.Console.WriteLine("Null");
        }

        return result;
    }

    public async Task LoginAsync(UserDto userDto)
    {
        var user = await this.FindByUserNameAsync(userDto.Email!);

        if (user is null)
        {
            throw new InvalidDataException();
        }

        var result = await this.signInManager.PasswordSignInAsync(user, userDto.Password!, true, true);
    }

    public async Task LogoutAsync() => await this.signInManager.SignOutAsync();
}