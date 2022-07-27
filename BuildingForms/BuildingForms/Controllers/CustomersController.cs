using BuildingForms.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildingForms.Controllers;

public class CustomersController : Controller {
    
    public IActionResult Index()
    {
        return View();
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Customer customer)
    {
        Console.WriteLine(customer);
        return View();
    }
}