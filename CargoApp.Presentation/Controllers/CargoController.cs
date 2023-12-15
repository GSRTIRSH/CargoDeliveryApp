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
    public async Task<IActionResult> Create(CargoDto cargo)
    {
        if (!ModelState.IsValid) return View(cargo);

        await _cargoService.CreateCargoRequest(cargo);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CargoDto cargo)
    {
        // валидируем все поля, если заявка новая
        if (cargo.Status == CargoStatus.New)
        {
            if (!ModelState.IsValid) return View(cargo);
        }

        await _cargoService.EditCargoRequest(cargo);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        await _cargoService.TransitCargoRequest(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _cargoService.DeleteCargoRequest(id);
        return RedirectToAction("Index");
    }
}