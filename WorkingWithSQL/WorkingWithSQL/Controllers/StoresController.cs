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

    public async Task<ActionResult> Detail(int id)
    {
        var store = await _storesServices.GetAsync(id);
        return store.Id==0 ? NotFound() : View(store);
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

    [HttpGet]
    public async Task<ActionResult> Update(int id)
    {
        var store = await _storesServices.GetAsync(id);
        return store.Id==0 ? NotFound() : View(store);
    }
    
    [HttpPost]
    public async Task<ActionResult> Update(Store store)
    {
        int rowsAffected = await _storesServices.UpdateAsync(store);
        Console.WriteLine($"rows affected: {rowsAffected}");
        return RedirectToAction("Detail", new{id=store.Id});
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        int rowsAffected = await _storesServices.DeleteAsync(id);
        Console.WriteLine($"rows affected: {rowsAffected}");
        return RedirectToAction("Index");
    }
}