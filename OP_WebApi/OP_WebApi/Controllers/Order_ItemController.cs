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
    public class Order_ItemController : ControllerBase
    {
        private readonly TableContext _context;

        public Order_ItemController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Order_Item
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Order_Item>>> GetOrder_Item()
        {
            return await _context.Order_Item.ToListAsync();
        }

        // GET: api/Order_Item/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Order_Item>> GetOrder_Item(long id)
        {
            var order_Item = await _context.Order_Item.FindAsync(id);

            if (order_Item == null)
            {
                return NotFound();
            }

            return order_Item;
        }

        // PUT: api/Order_Item/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutOrder_Item(long id, Order_Item order_Item)
        {
            if (id != order_Item.Id)
            {
                return BadRequest();
            }

            _context.Entry(order_Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_ItemExists(id))
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

        // POST: api/Order_Item
        [HttpPost, Authorize]
        public async Task<ActionResult<Order_Item>> PostOrder_Item(Order_Item order_Item)
        {
            _context.Order_Item.Add(order_Item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_Item", new { id = order_Item.Id }, order_Item);
        }

        // DELETE: api/Order_Item/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Order_Item>> DeleteOrder_Item(long id)
        {
            var order_Item = await _context.Order_Item.FindAsync(id);
            if (order_Item == null)
            {
                return NotFound();
            }

            _context.Order_Item.Remove(order_Item);
            await _context.SaveChangesAsync();

            return order_Item;
        }

        private bool Order_ItemExists(long id)
        {
            return _context.Order_Item.Any(e => e.Id == id);
        }
    }
}
