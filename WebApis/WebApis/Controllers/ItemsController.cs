using Microsoft.AspNetCore.Mvc;
using WebApis.Models;
using WebApis.Services;

namespace WebApis.Controllers;

[ApiController]
[Route("items")]
public class ItemsController : Controller
{
    private readonly ItemsService _itemsService;

    public ItemsController(ItemsService itemsService)
    {
        _itemsService = itemsService;
    }
    
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var items = await _itemsService.GetAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(string id)
    {
        var item = await _itemsService.GetAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult> Post(Item item)
    {
        await _itemsService.CreateAsync(item);
        
        // item has been assigned Id at this point
        return Created(new Uri($"items/{item.Id}", UriKind.Relative), item);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(string id, Item item)
    {
        var itemInDb = await _itemsService.GetAsync(id);
        if (itemInDb is null) return NotFound();

        item.Id = itemInDb.Id;
        await _itemsService.UpdateAsync(id, item);
        return Ok("OK");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var item = await _itemsService.GetAsync(id);
        if (item is null) return NotFound();

        await _itemsService.DeleteAsync(id);
        return Ok("OK");
    }
}