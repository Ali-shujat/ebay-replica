using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Models;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoredbsController : ControllerBase
    {
        private readonly commerceDBContext _context;

        public StoredbsController(commerceDBContext context)
        {
            _context = context;
        }

        // GET: api/Storedbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Storedb>>> GetStoredb()
        {
            return await _context.Storedb.ToListAsync();
        }

        // GET: api/Storedbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Storedb>> GetStoredb(string id)
        {
            var storedb = await _context.Storedb.FindAsync(id);

            if (storedb == null)
            {
                return NotFound();
            }

            return storedb;
        }

        // PUT: api/Storedbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoredb(string id, Storedb storedb)
        {
            if (id != storedb.Name)
            {
                return BadRequest();
            }

            _context.Entry(storedb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoredbExists(id))
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

        // POST: api/Storedbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Storedb>> PostStoredb(Storedb storedb)
        {
            _context.Storedb.Add(storedb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StoredbExists(storedb.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStoredb", new { id = storedb.Name }, storedb);
        }

        // DELETE: api/Storedbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoredb(string id)
        {
            var storedb = await _context.Storedb.FindAsync(id);
            if (storedb == null)
            {
                return NotFound();
            }

            _context.Storedb.Remove(storedb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoredbExists(string id)
        {
            return _context.Storedb.Any(e => e.Name == id);
        }
    }
}
