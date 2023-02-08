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
    public class ProductdbsController : ControllerBase
    {
        private readonly commerceDBContext _context;

        public ProductdbsController(commerceDBContext context)
        {
            _context = context;
        }

        // GET: api/Productdbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productdb>>> GetProductdb()
        {
            return await _context.Productdb.ToListAsync();
        }

        // GET: api/Productdbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Productdb>> GetProductdb(int id)
        {
            var productdb = await _context.Productdb.FindAsync(id);

            if (productdb == null)
            {
                return NotFound();
            }

            return productdb;
        }

        // PUT: api/Productdbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductdb(int id, Productdb productdb)
        {
            if (id != productdb.Id)
            {
                return BadRequest();
            }

            _context.Entry(productdb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductdbExists(id))
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

        // POST: api/Productdbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Productdb>> PostProductdb(Productdb productdb)
        {
            _context.Productdb.Add(productdb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductdbExists(productdb.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductdb", new { id = productdb.Id }, productdb);
        }

        // DELETE: api/Productdbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductdb(int id)
        {
            var productdb = await _context.Productdb.FindAsync(id);
            if (productdb == null)
            {
                return NotFound();
            }

            _context.Productdb.Remove(productdb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductdbExists(int id)
        {
            return _context.Productdb.Any(e => e.Id == id);
        }
    }
}
