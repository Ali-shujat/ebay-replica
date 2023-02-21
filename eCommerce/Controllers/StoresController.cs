using eCommerce.Data;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "super-admin")]
    public class StoresController : ControllerBase
    {
        private readonly ecommerceContext _context;
        private IStoreService _storeService;

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


        [HttpGet("{id}"), Authorize(Roles = "super-admin")]
        public async Task<ActionResult<Store>> GetStore(string id)
        {
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        [HttpPut("{id}"), Authorize(Roles = "super-admin,admin")]
        public async Task<IActionResult> PutStore(string id, Store store)
        {
            if (id != store.Name)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

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
        public async Task<ActionResult<Store>> PostStore(string storeName)
        {
            //_context.Stores.Add(store);
            try
            {
                // await _context.SaveChangesAsync();
                await _storeService.CreateStoreAsync(storeName);

            }
            catch (DbUpdateException)
            {
                if (StoreExists(storeName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return CreatedAtAction("GetStore", new { id = storeName }, storeName);
        }


        [HttpDelete("{name}"), Authorize(Roles = "super-admin")]
        public async Task<IActionResult> DeleteStore(string name)
        {
            var store = await _context.Stores.FindAsync(name);
            if (store != null && name != null)
            {
                await _storeService.DeleteStore(name);
                _context.Stores.Remove(store);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(string name)
        {
            return _context.Stores.Any(e => e.Name == name);
        }
    }
}
