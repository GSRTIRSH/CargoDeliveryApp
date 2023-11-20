using CargoApp.Application.DTOs;
using CargoApp.Application.Services.Interfaces;
using CargoApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClientApp.Controllers;

public class CargoController : Controller
{
    private readonly ICargoService _cargoService;

    public CargoController(ICargoService cargoService)
    {
        _cargoService = cargoService;
    }

    public IActionResult NotFound()
    {
        return View("NotFound");
    }
    
    public async Task<IActionResult> Index()
    {
        var cargoRequests = await _cargoService.GetCargoRequestsAsync();
        return View(cargoRequests);
    }

    public async Task<IActionResult> Search(string searchText, bool caseSensitive)
    {
        if (string.IsNullOrEmpty(searchText)) return RedirectToAction("Index");

        ViewBag.SearchText = searchText;
        ViewBag.SearchOption = caseSensitive;
        var searchResults = await _cargoService.SearchCargoRequestsAsync(searchText, caseSensitive);

        return View("Index", searchResults);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var cargo = await _cargoService.GetCargoRequestAsync(id);
        return cargo == null ? NotFound() : View(cargo);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CargoDto cargo)
    {
        if (!ModelState.IsValid) return View(cargo);

        _cargoService.CreateCargoRequest(cargo).Wait();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(CargoDto cargo)
    {
        // валидируем все поля, если заявка новая
        if (cargo.Status == CargoStatus.New)
        {
            if (!ModelState.IsValid) return View(cargo);
        }
        
        _cargoService.EditCargoRequest(cargo).Wait();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Approve(int id)
    {
        _cargoService.TransitCargoRequest(id).Wait();
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        _cargoService.DeleteCargoRequest(id).Wait();
        return RedirectToAction("Index");
    }
}