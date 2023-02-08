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
    public class UserdbsController : ControllerBase
    {
        private readonly commerceDBContext _context;

        public UserdbsController(commerceDBContext context)
        {
            _context = context;
        }

        // GET: api/Userdbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Userdb>>> GetUserdb()
        {
            return await _context.Userdb.ToListAsync();
        }

        // GET: api/Userdbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Userdb>> GetUserdb(int id)
        {
            var userdb = await _context.Userdb.FindAsync(id);

            if (userdb == null)
            {
                return NotFound();
            }

            return userdb;
        }

        // PUT: api/Userdbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserdb(int id, Userdb userdb)
        {
            if (id != userdb.Id)
            {
                return BadRequest();
            }

            _context.Entry(userdb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserdbExists(id))
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

        // POST: api/Userdbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Userdb>> PostUserdb(Userdb userdb)
        {
            _context.Userdb.Add(userdb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserdbExists(userdb.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserdb", new { id = userdb.Id }, userdb);
        }

        // DELETE: api/Userdbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserdb(int id)
        {
            var userdb = await _context.Userdb.FindAsync(id);
            if (userdb == null)
            {
                return NotFound();
            }

            _context.Userdb.Remove(userdb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserdbExists(int id)
        {
            return _context.Userdb.Any(e => e.Id == id);
        }
    }
}
