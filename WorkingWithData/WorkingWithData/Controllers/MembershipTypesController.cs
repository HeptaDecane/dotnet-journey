using Microsoft.AspNetCore.Mvc;
using WorkingWithData.Models;
using WorkingWithData.Services;

namespace WorkingWithData.Controllers;

public class MembershipTypesController : Controller {
    private readonly MembershipTypesService _membershipTypesService;

    public MembershipTypesController(MembershipTypesService membershipTypesService) {
        _membershipTypesService = membershipTypesService;
    }
    
    // GET
    public async Task<ActionResult> Index()
    {
        var membershipTypes = await _membershipTypesService.GetAsync();
        return View(membershipTypes);
    }
}