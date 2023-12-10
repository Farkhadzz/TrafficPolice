using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TrafficPoliceMVC.Models;
using TrafficPoliceMVC.Sql;

public class UserController : Controller
{
    private readonly AppDbContext _dbContext;

    public UserController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(UserModel user)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        return View(user);
    }
}