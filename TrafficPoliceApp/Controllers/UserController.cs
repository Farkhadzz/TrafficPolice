namespace TrafficPoliceApp.Controllers;

using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    [HttpGet]
    public IActionResult UserInfo()
    {
        var claims = base.User.Claims;

        return base.View(model: claims);
    }
}