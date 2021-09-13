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
    public class ItemsController : ControllerBase
    {
        private readonly TableContext _context;

        public ItemsController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Items?all=vvv&company_Id=xxx&EnableType=yyy
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem
            (string all = "yes", long company_id = 0, int EnableType = 0)
        {
            if (all.Equals("yes"))
                return await _context.Item.ToListAsync();
            else
            {
                List<Item> lstItems = await _context.Item.Where(d => d.Company_Id == company_id).ToListAsync();
                if (EnableType == 1) return lstItems.Where(d => d.Enable).ToList();
                else if (EnableType == -1) return lstItems.Where(d => !d.Enable).ToList();
                else return lstItems;
            }
        }

        // GET: api/Items/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Item>> GetItem(long id)
        {
            var item = await _context.Item.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutItem(long id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [HttpPost, Authorize]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Item.Add(item);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetItem", new { id = item.Id }, item);
            return item;
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Item>> DeleteItem(long id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(long id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
