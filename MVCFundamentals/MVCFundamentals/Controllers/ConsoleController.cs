using Microsoft.AspNetCore.Mvc;
using MVCFundamentals.Models;
using MVCFundamentals.ViewModels;

namespace MVCFundamentals.Controllers;

public class ConsoleController : Controller {
    // GET
    public ActionResult Index() {
        return View();
    }

    // MVC in action
    public ActionResult Random() {
        var console = new ConsoleViewModel() { Id = 101, Message = "Hello World!!" };
        return View(console);
    }

    // common ActionResult(s)
    public ActionResult Action01() {
        // return Content("Hello World");
        // return RedirectToAction("Index", "Home");
        // return Redirect("/console");
        return new EmptyResult();
    }

    // action with id param
    // /action02/<id> or /action02?id=<id>
    public ActionResult Action02(int id) {
        return Content($"id: {id}");
    }
    
    // action with some query params
    // /action03?page=<page>&sortBy=<sortBy>
    public ActionResult Action03(int? page, string? sortBy) {
        page ??= 1;
        sortBy ??= "Name";
        return Content($"page: {page}, sortBy:{sortBy}");
    }

    // action for custom routes
    public ActionResult Action04(int vol, int page) {
        return Content($"vol: {vol}, page: {page}");
    }
    
    // view models
    public ActionResult Action05() {
        var movie = new Movie() {Id=42, Title = "Shrek"};
        var customers = new List<Customer> {
            new Customer(){Id=17, Name="Jim"},
            new Customer(){Id=23, Name="Near"},
            new Customer(){Id=67, Name="Ross"}
        };

        var viewModel = new Action05MovieViewModel() {
            Movie = movie,
            Customers = customers
        };
        
        return View(viewModel);
    }
}