using Microsoft.AspNetCore.Mvc;
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
}