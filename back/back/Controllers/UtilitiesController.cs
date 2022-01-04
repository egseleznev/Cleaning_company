using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRSeleznev.Data.Models;

namespace LRSeleznev.Data.Contexts
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        private readonly SystemContext _context;

        public UtilitiesController(SystemContext context)
        {
            _context = context;
        }

        // GET: api/Utilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utility>>> GetUtility()
        {
            return await _context.Utility.ToListAsync();
        }

        // GET: api/Utilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utility>> GetUtility(int id)
        {
            var utility = await _context.Utility.FindAsync(id);

            if (utility == null)
            {
                return NotFound();
            }

            return utility;
        }

        // PUT: api/Utilities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtility(int id, Utility utility)
        {
            if (id != utility.id)
            {
                return BadRequest();
            }

            _context.Entry(utility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilityExists(id))
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

        // POST: api/Utilities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Utility>> PostUtility(Utility utility)
        {
            _context.Utility.Add(utility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtility", new { id = utility.id }, utility);
        }

        // DELETE: api/Utilities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Utility>> DeleteUtility(int id)
        {
            var utility = await _context.Utility.FindAsync(id);
            if (utility == null)
            {
                return NotFound();
            }

            _context.Utility.Remove(utility);
            await _context.SaveChangesAsync();

            return utility;
        }

        private bool UtilityExists(int id)
        {
            return _context.Utility.Any(e => e.id == id);
        }
    }
}
