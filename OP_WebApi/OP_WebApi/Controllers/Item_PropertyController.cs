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
    public class Item_PropertyController : ControllerBase
    {
        private readonly TableContext _context;

        public Item_PropertyController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Item_Property?all=vvv&company_Id=xxx&Item_Id=yyy&Property_Id=zzz
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Item_Property>>> GetItem_Property
            (string all = "yes", long company_Id = 0,long Item_Id=0,long Property_Id=0)

        {
            if(all.Equals("yes"))
                return await _context.Item_Property.ToListAsync();
            else
            {
                List<Item_Property> lstIP = await _context.Item_Property.Where(d=>d.Company_Id==company_Id).ToListAsync();
                if (Item_Id > 0) lstIP = lstIP.Where(d => d.Item_Id == Item_Id).ToList();
                if (Property_Id > 0) lstIP = lstIP.Where(d => d.Property_Index == Property_Id).ToList();
                return lstIP;
            }
        }

        // GET: api/Item_Property/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Item_Property>> GetItem_Property(long id)
        {
            var item_Property = await _context.Item_Property.FindAsync(id);

            if (item_Property == null)
            {
                return NotFound();
            }

            return item_Property;
        }

        // PUT: api/Item_Property/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutItem_Property(long id, Item_Property item_Property)
        {
            if (id != item_Property.Id)
            {
                return BadRequest();
            }

            _context.Entry(item_Property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Item_PropertyExists(id))
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

        // POST: api/Item_Property
        [HttpPost, Authorize]
        public async Task<ActionResult<Item_Property>> PostItem_Property(Item_Property item_Property)
        {
            _context.Item_Property.Add(item_Property);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem_Property", new { id = item_Property.Id }, item_Property);
        }

        // DELETE: api/Item_Property/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Item_Property>> DeleteItem_Property(long id)
        {
            var item_Property = await _context.Item_Property.FindAsync(id);
            if (item_Property == null)
            {
                return NotFound();
            }

            _context.Item_Property.Remove(item_Property);
            await _context.SaveChangesAsync();

            return item_Property;
        }

        private bool Item_PropertyExists(long id)
        {
            return _context.Item_Property.Any(e => e.Id == id);
        }
    }
}
