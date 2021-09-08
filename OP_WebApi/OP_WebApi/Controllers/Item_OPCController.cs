using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OP_WebApi.Models;

namespace OP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Item_OPCController : ControllerBase
    {
        private readonly TableContext _context;

        public Item_OPCController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Item_OPC
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Item_OPC>>> GetItem_OPC()
        {
            return await _context.Item_OPC.ToListAsync();
        }

        // GET: api/Item_OPC/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Item_OPC>> GetItem_OPC(long id)
        {
            var item_OPC = await _context.Item_OPC.FindAsync(id);

            if (item_OPC == null)
            {
                return NotFound();
            }

            return item_OPC;
        }

        // PUT: api/Item_OPC/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutItem_OPC(long id, Item_OPC item_OPC)
        {
            if (id != item_OPC.Id)
            {
                return BadRequest();
            }

            _context.Entry(item_OPC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Item_OPCExists(id))
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

        // POST: api/Item_OPC
        [HttpPost, Authorize]
        public async Task<ActionResult<Item_OPC>> PostItem_OPC(Item_OPC item_OPC)
        {
            _context.Item_OPC.Add(item_OPC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem_OPC", new { id = item_OPC.Id }, item_OPC);
        }

        // DELETE: api/Item_OPC/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Item_OPC>> DeleteItem_OPC(long id)
        {
            var item_OPC = await _context.Item_OPC.FindAsync(id);
            if (item_OPC == null)
            {
                return NotFound();
            }

            _context.Item_OPC.Remove(item_OPC);
            await _context.SaveChangesAsync();

            return item_OPC;
        }

        private bool Item_OPCExists(long id)
        {
            return _context.Item_OPC.Any(e => e.Id == id);
        }
    }
}
