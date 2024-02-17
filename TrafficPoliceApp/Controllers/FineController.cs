namespace TrafficPoliceApp.Controllers;

using TrafficPoliceApp.Dtos;
using TrafficPoliceApp.Models;
using TrafficPoliceApp.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

[Route("/[controller]")]
public class FineController : Controller
{
    private readonly IFineRepository fineRepository;

    public FineController(IFineRepository fineRepository) {
        this.fineRepository = fineRepository;
    }

    [HttpGet]
    [ActionName("Index")]
    public async Task<IActionResult> ShowAll()
    {
        var fines = await this.fineRepository.GetAllAsync();

        return View(model: fines);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult Create() {
        return View();
    }

    // [HttpPost]
    // public async Task<IActionResult> Create([FromForm] FineDto dto)
    // {
    //     await this.fineRepository.InsertFineAsync(new Fine
    //     {
    //         FineName = dto.FineName,
    //         Price = dto.Price,
    //     });

    //     return RedirectToAction("Index");
    // }
}