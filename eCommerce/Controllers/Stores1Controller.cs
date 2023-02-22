using eCommerce.Data;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Stores1Controller : ControllerBase
    {
        private readonly ecommerceContext _context;
        private readonly IStoreService _storeService;

        public Stores1Controller(ecommerceContext context, IStoreService storeService)
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
            var store = await _context.Stores.FirstOrDefaultAsync(c => c.Id == id);
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

        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(string nyaStoreNam)
        {
            var oldstore = await _context.Stores.FirstOrDefaultAsync(o => o.Name == nyaStoreNam);
            if (oldstore != null)
            {
                return BadRequest();
            }

            var nyaStore = new Store() { Name = nyaStoreNam, UniqueStoreId = _context.Stores.Count() * 2 };
            _context.Stores.Add(nyaStore);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = nyaStore.Id }, nyaStore);

        }

        // DELETE: api/Stores1/5
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
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
