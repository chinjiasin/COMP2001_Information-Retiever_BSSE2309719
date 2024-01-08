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
    public class BookMarksController : ControllerBase
    {
        private readonly COMP2001MAL_CJiasinContext _context;

        public BookMarksController(COMP2001MAL_CJiasinContext context)
        {
            _context = context;
        }

        // GET: api/BookMarks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookMark>>> GetBookMarks()
        {
          if (_context.BookMarks == null)
          {
              return NotFound();
          }
            return await _context.BookMarks.ToListAsync();
        }

        // GET: api/BookMarks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookMark>> GetBookMark(int id)
        {
          if (_context.BookMarks == null)
          {
              return NotFound();
          }
            var bookMark = await _context.BookMarks.FindAsync(id);

            if (bookMark == null)
            {
                return NotFound();
            }

            return bookMark;
        }

        // PUT: api/BookMarks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookMark(int id, BookMark bookMark)
        {
            if (id != bookMark.BookMarkId)
            {
                return BadRequest();
            }

            _context.Entry(bookMark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookMarkExists(id))
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

        // POST: api/BookMarks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookMark>> PostBookMark(BookMark bookMark)
        {
          if (_context.BookMarks == null)
          {
              return Problem("Entity set 'COMP2001MAL_CJiasinContext.BookMarks'  is null.");
          }
            _context.BookMarks.Add(bookMark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookMark", new { id = bookMark.BookMarkId }, bookMark);
        }

        // DELETE: api/BookMarks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookMark(int id)
        {
            if (_context.BookMarks == null)
            {
                return NotFound();
            }
            var bookMark = await _context.BookMarks.FindAsync(id);
            if (bookMark == null)
            {
                return NotFound();
            }

            _context.BookMarks.Remove(bookMark);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookMarkExists(int id)
        {
            return (_context.BookMarks?.Any(e => e.BookMarkId == id)).GetValueOrDefault();
        }
    }
}
