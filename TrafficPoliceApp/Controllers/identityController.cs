using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;

namespace TrafficPoliceApp.Controllers;

public class IdentityController : Controller
{
    private readonly IDataProtector dataProtector;

    private readonly IUserRepository userRepository;

    public IdentityController(IDataProtectionProvider dataProtectionProvider, IUserRepository userRepository)
    {
        this.dataProtector = dataProtectionProvider.CreateProtector("Context");
        this.userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return base.View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return base.View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserDto userDto)
    {
        var user = await userRepository.GetUser(userDto);

        if (user is not null)
        {
            var hash = this.dataProtector.Protect(user.Id.ToString());

            base.HttpContext.Response.Cookies.Append("Authorize", hash);

            return RedirectToAction("Index", "Fine");
        }
        else return BadRequest("Wrong Data");
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] User user)
    {
        await userRepository.InsertUserAsync(new Models.User{
            Email = user.Email,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Age = user.Age
        });
        return base.RedirectToAction("Login");
    }
}