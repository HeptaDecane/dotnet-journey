using Microsoft.AspNetCore.Mvc;
using WorkingWithSQL.Models;
using WorkingWithSQL.Services;

namespace WorkingWithSQL.Controllers;

public class StoresController : Controller
{
    private readonly StoresServices _storesServices;

    public StoresController(StoresServices storesServices) {
        _storesServices = storesServices;
    }
    
    public async Task<ActionResult> Index()
    {
        var stores = await _storesServices.GetAsync();
        return View(stores);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Store store)
    {
        int rowsAffected = await _storesServices.CreateAsync(store);
        Console.WriteLine($"rows affected: {rowsAffected}");
        return RedirectToAction("Index");
    }
}