using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using TrafficPoliceApp.Services.Base;

namespace TrafficPoliceApp.Controllers;

public class IdentityController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly IIdentityService identityService;

    public IdentityController(IUserRepository userRepository, IIdentityService identityService)
    {
        this.userRepository = userRepository;
        this.identityService = identityService;
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

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] UserDto userDto)
    {
        try {
            await this.identityService.LoginAsync(userDto);
        }
        catch(Exception ex)
        {
            return base.BadRequest(ex.Message);
        }
        return base.RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] UserDto userDto)
    {
        var result = await this.identityService.RegisterAsync(userDto);

        return base.RedirectToAction("Login");
    }

    [HttpDelete]
    public async Task<IActionResult> Logout()
    {
        await this.identityService.LogoutAsync();

        return RedirectToAction("Index", "Home");
    }
}