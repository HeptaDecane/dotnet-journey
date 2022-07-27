using BuildingForms.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildingForms.Controllers;

public class BooksController : Controller
{
    // GET
    public IActionResult Index() {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        Console.WriteLine(book);
        return View(book);
    }
}