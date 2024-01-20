namespace TrafficPoliceApp.Controllers;

using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

[Route("/[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository userRepository;

    public UserController(IUserRepository userRepository) {
        this.userRepository = userRepository;
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> ShowAll()
    {
        var users = await this.userRepository.GetAllAsync();

        return View(model: users);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult Create() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm]UserDto dto) {
        await this.userRepository.InsertUserAsync(new User {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email
        });

        return RedirectToAction("Index");
    }
}