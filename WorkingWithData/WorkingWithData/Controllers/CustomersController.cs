using Microsoft.AspNetCore.Mvc;
using WorkingWithData.Services;
using WorkingWithData.ViewModels;

namespace WorkingWithData.Controllers;

public class CustomersController : Controller
{
    private readonly CustomersService _customersService;
    private readonly MembershipTypesService _membershipTypesService;

    public CustomersController(CustomersService customersService, MembershipTypesService membershipTypesService)
    {
        _customersService = customersService;
        _membershipTypesService = membershipTypesService;
    }
    
    // GET
    public async Task<ActionResult> Index()
    {
        var customers = await _customersService.GetAsync();
        return View(customers);
    }

    public async Task<ActionResult> Get(int id)
    {
        var customer = await _customersService.GetAsync(id);
        var membershipType = await _membershipTypesService.GetAsync(customer.MembershipTypeId);
        var getCustomerViewModel = new GetCustomerViewModel() {
            Customer = customer,
            MembershipType = membershipType
        };
        return View(getCustomerViewModel);
    }
}