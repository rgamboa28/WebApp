using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchDetailsController : ControllerBase
    {
        private readonly APIContext _context;

        public WatchDetailsController(APIContext context)
        {
            _context = context;
        }

        // GET: api/WatchDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchDetails>>> GetWatchDetails()
        {
            return await _context.WatchDetails.ToListAsync();
        }

        // GET: api/WatchDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WatchDetails>> GetWatchDetails(int id)
        {
            var watchDetails = await _context.WatchDetails.FindAsync(id);

            if (watchDetails == null)
            {
                return NotFound();
            }

            return watchDetails;
        }

        // PUT: api/WatchDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWatchDetails(int id, WatchDetails watchDetails)
        {
            if (id != watchDetails.WatchId)
            {
                return BadRequest();
            }

            _context.Entry(watchDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchDetailsExists(id))
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

        // POST: api/WatchDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WatchDetails>> PostWatchDetails(WatchDetails watchDetails)
        {
            _context.WatchDetails.Add(watchDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWatchDetails", new { id = watchDetails.WatchId }, watchDetails);
        }

        // DELETE: api/WatchDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WatchDetails>> DeleteWatchDetails(int id)
        {
            var watchDetails = await _context.WatchDetails.FindAsync(id);
            if (watchDetails == null)
            {
                return NotFound();
            }

            _context.WatchDetails.Remove(watchDetails);
            await _context.SaveChangesAsync();

            return watchDetails;
        }

        private bool WatchDetailsExists(int id)
        {
            return _context.WatchDetails.Any(e => e.WatchId == id);
        }
    }
}
