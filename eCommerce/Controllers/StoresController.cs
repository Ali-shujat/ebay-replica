using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
public class StoresController : ControllerBase
{
    private readonly ecommerceContext _context;
    private readonly IStoreService _storeService;

    public StoresController(ecommerceContext context, IStoreService storeService)
    {
        _context = context;
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Store>>> GetStores()
    {
        return await _context.Stores.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Store>> GetStore(int id)
    {
        var store = await _context.Stores.FindAsync(id);

        if (store == null)
        {
            return NotFound();
        }

        return store;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStore(int id, string name)
    {
        var store = await _context.Stores.FirstOrDefaultAsync(c => c.StoreId == id);
        if (store != null && name != null)
        {
            store.Name = name;
        }

        _context.Entry(store).State = EntityState.Modified;
        _context.Update(store);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StoreExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost, Authorize(Roles = "super-admin")]
    public async Task<ActionResult<Store>> PostStore(string nyaStoreNam)
    {
        var oldstore = await _context.Stores.FirstOrDefaultAsync(o => o.Name == nyaStoreNam);
        if (oldstore != null)
        {
            return BadRequest();
        }

        var nyaStore = new Store() { Name = nyaStoreNam };
        _context.Stores.Add(nyaStore);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStore", new { id = nyaStore.StoreId }, nyaStore);

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
        var store = await _context.Stores.FindAsync(id);
        if (store == null)
        {
            return NotFound();
        }
        _storeService.DeleteStore(store);
        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StoreExists(int id)
    {
        return _context.Stores.Any(e => e.StoreId == id);
    }


}
