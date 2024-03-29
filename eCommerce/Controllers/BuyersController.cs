﻿using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        private readonly ecommerceContext _context;

        public BuyersController(ecommerceContext context)
        {
            _context = context;
        }

        // GET: api/Buyers
        [HttpGet, Authorize(Roles = "super-admin")]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyers()
        {
            return await _context.Buyers.ToListAsync();
        }

        // GET: api/Buyers/5
        [HttpGet("{id}"), Authorize(Roles = "super-admin")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
            var buyer = await _context.Buyers.FindAsync(id);

            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        // PUT: api/Buyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(int id, Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return BadRequest();
            }

            _context.Entry(buyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerExists(id))
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

        // POST: api/Buyers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)
        {
            _context.Buyers.Add(buyer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BuyerExists(buyer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            var buyer = await _context.Buyers.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }

            _context.Buyers.Remove(buyer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuyerExists(int id)
        {
            return _context.Buyers.Any(e => e.Id == id);
        }
    }
}
