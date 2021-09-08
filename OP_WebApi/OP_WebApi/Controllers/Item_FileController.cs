using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OP_WebApi.Models;

namespace OP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Item_FileController : ControllerBase
    {
        private readonly TableContext _context;

        public Item_FileController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Item_File
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Item_File>>> GetItem_File()
        {
            return await _context.Item_File.ToListAsync();
        }

        // GET: api/Item_File/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Item_File>> GetItem_File(long id)
        {
            var item_File = await _context.Item_File.FindAsync(id);

            if (item_File == null)
            {
                return NotFound();
            }

            return item_File;
        }

        // PUT: api/Item_File/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutItem_File(long id, Item_File item_File)
        {
            if (id != item_File.Id)
            {
                return BadRequest();
            }

            _context.Entry(item_File).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Item_FileExists(id))
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

        // POST: api/Item_File
        [HttpPost, Authorize]
        public async Task<ActionResult<Item_File>> PostItem_File(Item_File item_File)
        {
            _context.Item_File.Add(item_File);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem_File", new { id = item_File.Id }, item_File);
        }

        // DELETE: api/Item_File/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Item_File>> DeleteItem_File(long id)
        {
            var item_File = await _context.Item_File.FindAsync(id);
            if (item_File == null)
            {
                return NotFound();
            }

            _context.Item_File.Remove(item_File);
            await _context.SaveChangesAsync();

            return item_File;
        }

        private bool Item_FileExists(long id)
        {
            return _context.Item_File.Any(e => e.Id == id);
        }
    }
}
