using Microsoft.AspNetCore.Identity;
using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;

namespace TrafficPoliceApp.Services.Base;

public interface IIdentityService 
{
    public Task<User?> FindByUserNameAsync(string userName);
    Task<IdentityResult> RegisterAsync(UserDto userDto);
    Task LoginAsync(UserDto userDto);
    Task LogoutAsync();
}