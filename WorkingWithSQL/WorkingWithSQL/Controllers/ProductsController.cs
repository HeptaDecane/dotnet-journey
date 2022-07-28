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

    public async Task<ActionResult> Detail(int id)
    {
        var product = await _productsServices.GetAsync(id);
        return product.Id == 0 ? NotFound() : View(product);
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

    [HttpGet]
    public async Task<ActionResult> Update(int id)
    {
        var product = await _productsServices.GetAsync(id);
        return product.Id == 0 ? NotFound() : View(product);
    }

    [HttpPost]
    public async Task<ActionResult> Update(Product product)
    {
        int rowsAffected = await _productsServices.UpdateAsync(product);
        Console.WriteLine($"rows affected: {rowsAffected}");
        return RedirectToAction("Detail", new {id=product.Id});
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        int rowsAffected = await _productsServices.DeleteAsync(id);
        Console.WriteLine($"rows affected: {rowsAffected}");
        return RedirectToAction("Index");
    }
}