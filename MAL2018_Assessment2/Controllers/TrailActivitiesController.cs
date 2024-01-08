using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MAL2018_Assessment2.Models;

namespace MAL2018_Assessment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrailActivitiesController : ControllerBase
    {
        private readonly COMP2001MAL_CJiasinContext _context;

        public TrailActivitiesController(COMP2001MAL_CJiasinContext context)
        {
            _context = context;
        }

        // GET: api/TrailActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrailActivity>>> GetTrailActivities()
        {
          if (_context.TrailActivities == null)
          {
              return NotFound();
          }
            return await _context.TrailActivities.ToListAsync();
        }

        // GET: api/TrailActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrailActivity>> GetTrailActivity(int id)
        {
          if (_context.TrailActivities == null)
          {
              return NotFound();
          }
            var trailActivity = await _context.TrailActivities.FindAsync(id);

            if (trailActivity == null)
            {
                return NotFound();
            }

            return trailActivity;
        }

        // PUT: api/TrailActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrailActivity(int id, TrailActivity trailActivity)
        {
            if (id != trailActivity.TrailActivitiesId)
            {
                return BadRequest();
            }

            _context.Entry(trailActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrailActivityExists(id))
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

        // POST: api/TrailActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrailActivity>> PostTrailActivity(TrailActivity trailActivity)
        {
          if (_context.TrailActivities == null)
          {
              return Problem("Entity set 'COMP2001MAL_CJiasinContext.TrailActivities'  is null.");
          }
            _context.TrailActivities.Add(trailActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrailActivity", new { id = trailActivity.TrailActivitiesId }, trailActivity);
        }

        // DELETE: api/TrailActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrailActivity(int id)
        {
            if (_context.TrailActivities == null)
            {
                return NotFound();
            }
            var trailActivity = await _context.TrailActivities.FindAsync(id);
            if (trailActivity == null)
            {
                return NotFound();
            }

            _context.TrailActivities.Remove(trailActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrailActivityExists(int id)
        {
            return (_context.TrailActivities?.Any(e => e.TrailActivitiesId == id)).GetValueOrDefault();
        }
    }
}
