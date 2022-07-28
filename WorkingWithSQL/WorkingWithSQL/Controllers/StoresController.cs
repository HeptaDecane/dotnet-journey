using Microsoft.AspNetCore.Mvc;
using WorkingWithSQL.Services;

namespace WorkingWithSQL.Controllers;

public class StoresController : Controller
{
    private readonly StoresServices _storesServices;

    public StoresController(StoresServices storesServices) {
        _storesServices = storesServices;
    }
    
    public IActionResult Index()
    {
        var stores = _storesServices.Get();
        return View(stores);
    }
}