// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
// using TrafficPoliceApp.Models;
// using TrafficPoliceApp.Repositories.Base;

// namespace TrafficPoliceApp.Controllers
// {
//     public class AdminController : Controller
//     {
//         private readonly IUserRepository _userRepository;
//         private readonly IFineRepository _fineRepository;

//         public AdminController(IUserRepository userRepository, IFineRepository fineRepository)
//         {
//             _userRepository = userRepository;
//             _fineRepository = fineRepository;
//         }

//         public async Task<IActionResult> Index()
//         {
//             var users = await _userRepository.GetAllAsync();
//             return View(users);
//         }

//         [HttpGet]
//         public async Task<IActionResult> AddFine(int userId)
//         {
//             var fineColumns = await _fineRepository.GetColumnsAsync();
//             ViewBag.Columns = fineColumns;
//             ViewBag.UserId = userId;
//             return View();
//         }

//         [HttpPost]
//         public async Task<IActionResult> AddFine([FromForm] Fine fine)
//         {
//             var userId = int.Parse(Request.Form["userId"]);
//             Console.WriteLine($"Adding fine for user with Id: {userId}");
//             await _fineRepository.InsertFineForUserAsync(fine, userId);
//             return RedirectToAction("Index");
//         }
//     }
// }
