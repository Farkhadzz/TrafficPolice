using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace TrafficPoliceApp.Controllers;

public class IdentityController : Controller
{
    private readonly IUserRepository userRepository;

    public IdentityController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        base.ViewData["returnUrl"] = returnUrl;

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
        var user = await userRepository.GetUser(userDto);

        if (user is not null)   
        {
            if (user.Email == "admin@admin.com" && user.Password == "admin")
            {
                var claims = new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim("Admin", "true")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: new ClaimsPrincipal(claimsIdentity)
                );

                return RedirectToAction("Index", "Home");
            }
            else
            {
                var claims = new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: new ClaimsPrincipal(claimsIdentity)
                );

                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            return BadRequest("Wrong Data");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] User user)
    {
        if (user != null && !string.IsNullOrEmpty(user.Password) && !string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
        {
            if (user.Email == "admin@admin.com" && user.Password == "admin")
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim("Admin", "true")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: new ClaimsPrincipal(claimsIdentity)
                );
            }

            await userRepository.InsertUserAsync(new Models.User
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            });
            return base.RedirectToAction("Login");
        }
        else return BadRequest("Wrong Data");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }
}   