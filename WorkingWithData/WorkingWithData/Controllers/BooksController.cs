using Microsoft.AspNetCore.Mvc;
using WorkingWithData.Services;
using WorkingWithData.Models;

namespace WorkingWithData.Controllers;

public class BooksController : Controller {
    private readonly BooksService _booksService;
    
    public BooksController(BooksService booksService) {
        _booksService = booksService;
    }

    public async Task<ActionResult> Get(int id)
    {
        if (id == 0)
            return RedirectToAction("Index");
        var book = await _booksService.GetAsync(id);
        return View(book);
    }
    
    public async Task<IActionResult> Index() {
        var books = await _booksService.GetAsync();
        return View(books);
    }
}