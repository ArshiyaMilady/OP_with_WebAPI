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
    public class Order_LevelController : ControllerBase
    {
        private readonly TableContext _context;

        public Order_LevelController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Order_Level
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Order_Level>>> GetOrder_Level()
        {
            return await _context.Order_Level.ToListAsync();
        }

        // GET: api/Order_Level/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Order_Level>> GetOrder_Level(long id)
        {
            var order_Level = await _context.Order_Level.FindAsync(id);

            if (order_Level == null)
            {
                return NotFound();
            }

            return order_Level;
        }

        // PUT: api/Order_Level/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutOrder_Level(long id, Order_Level order_Level)
        {
            if (id != order_Level.Id)
            {
                return BadRequest();
            }

            _context.Entry(order_Level).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_LevelExists(id))
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

        // POST: api/Order_Level
        [HttpPost, Authorize]
        public async Task<ActionResult<Order_Level>> PostOrder_Level(Order_Level order_Level)
        {
            _context.Order_Level.Add(order_Level);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_Level", new { id = order_Level.Id }, order_Level);
        }

        // DELETE: api/Order_Level/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Order_Level>> DeleteOrder_Level(long id)
        {
            var order_Level = await _context.Order_Level.FindAsync(id);
            if (order_Level == null)
            {
                return NotFound();
            }

            _context.Order_Level.Remove(order_Level);
            await _context.SaveChangesAsync();

            return order_Level;
        }

        private bool Order_LevelExists(long id)
        {
            return _context.Order_Level.Any(e => e.Id == id);
        }
    }
}
