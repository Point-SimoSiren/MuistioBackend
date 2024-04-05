using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuistioBackend.Data;
using MuistioBackend.Models;

namespace MuistioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemosController : ControllerBase
    {
        // Alustetaan tyhjänä
        private readonly ApplicationDbContext _context;

        // Konstruktorimetodissa avatataan mahdollisuus injektoida
        // DbContext context -tyyppinen tieto ja asettaa se tyhjänä yllä alustettuun paikkaan
        // injektoinnin tekee program.cs
        public MemosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Memos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Memo>>> GetMemos()
        {
          if (_context.Memos == null)
          {
              return NotFound();
          }
            return await _context.Memos.ToListAsync();
        }

        // GET: api/Memos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memo>> GetMemo(int id)
        {
          if (_context.Memos == null)
          {
              return NotFound();
          }
            var memo = await _context.Memos.FindAsync(id);

            if (memo == null)
            {
                return NotFound();
            }

            return memo;
        }

        // PUT: api/Memos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemo(int id, Memo memo)
        {
            if (id != memo.Id)
            {
                return BadRequest();
            }

            _context.Entry(memo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoExists(id))
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

        // POST: api/Memos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Memo>> PostMemo(Memo memo)
        {
          if (_context.Memos == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Memos'  is null.");
          }
            _context.Memos.Add(memo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemo", new { id = memo.Id }, memo);
        }

        // DELETE: api/Memos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemo(int id)
        {
            if (_context.Memos == null)
            {
                return NotFound();
            }
            var memo = await _context.Memos.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }

            _context.Memos.Remove(memo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemoExists(int id)
        {
            return (_context.Memos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
