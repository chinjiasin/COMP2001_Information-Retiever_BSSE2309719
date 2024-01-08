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
    public class ProfileMarksController : ControllerBase
    {
        private readonly COMP2001MAL_CJiasinContext _context;

        public ProfileMarksController(COMP2001MAL_CJiasinContext context)
        {
            _context = context;
        }

        // GET: api/ProfileMarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileMark>>> GetProfileMarks()
        {
          if (_context.ProfileMarks == null)
          {
              return NotFound();
          }
            return await _context.ProfileMarks.ToListAsync();
        }

        // GET: api/ProfileMarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileMark>> GetProfileMark(int id)
        {
          if (_context.ProfileMarks == null)
          {
              return NotFound();
          }
            var profileMark = await _context.ProfileMarks.FindAsync(id);

            if (profileMark == null)
            {
                return NotFound();
            }

            return profileMark;
        }

        // PUT: api/ProfileMarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfileMark(int id, ProfileMark profileMark)
        {
            if (id != profileMark.ProfileMarkId)
            {
                return BadRequest();
            }

            _context.Entry(profileMark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileMarkExists(id))
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

        // POST: api/ProfileMarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProfileMark>> PostProfileMark(ProfileMark profileMark)
        {
          if (_context.ProfileMarks == null)
          {
              return Problem("Entity set 'COMP2001MAL_CJiasinContext.ProfileMarks'  is null.");
          }
            _context.ProfileMarks.Add(profileMark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfileMark", new { id = profileMark.ProfileMarkId }, profileMark);
        }

        // DELETE: api/ProfileMarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfileMark(int id)
        {
            if (_context.ProfileMarks == null)
            {
                return NotFound();
            }
            var profileMark = await _context.ProfileMarks.FindAsync(id);
            if (profileMark == null)
            {
                return NotFound();
            }

            _context.ProfileMarks.Remove(profileMark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileMarkExists(int id)
        {
            return (_context.ProfileMarks?.Any(e => e.ProfileMarkId == id)).GetValueOrDefault();
        }
    }
}
