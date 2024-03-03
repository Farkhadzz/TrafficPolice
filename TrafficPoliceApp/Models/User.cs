using Microsoft.AspNetCore.Identity;

namespace TrafficPoliceApp.Models;

public class User : IdentityUser {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}