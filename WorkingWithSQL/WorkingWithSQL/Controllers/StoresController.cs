using Microsoft.AspNetCore.Mvc;
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
}