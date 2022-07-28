using Microsoft.AspNetCore.Mvc;
using WorkingWithSQL.Models;
using WorkingWithSQL.Services;

namespace WorkingWithSQL.Controllers;

public class ProductsController : Controller
{
    private readonly ProductsServices _productsServices;

    public ProductsController(ProductsServices productsServices)
    {
        _productsServices = productsServices;
    }
    
    public async Task<ActionResult> Index()
    {
        var products = await _productsServices.GetAsync();
        return View(products);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Product product)
    {
        int rowsAffected = await _productsServices.CreateAsync(product);
        Console.WriteLine($"rows affected: {rowsAffected}");
        return RedirectToAction("Index");
    }
}