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
    public class Order_HistoryController : ControllerBase
    {
        private readonly TableContext _context;

        public Order_HistoryController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Order_History
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order_History>>> GetOrder_History()
        {
            return await _context.Order_History.ToListAsync();
        }

        // GET: api/Order_History/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order_History>> GetOrder_History(int id)
        {
            var order_History = await _context.Order_History.FindAsync(id);

            if (order_History == null)
            {
                return NotFound();
            }

            return order_History;
        }

        // PUT: api/Order_History/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder_History(int id, Order_History order_History)
        {
            if (id != order_History.Id)
            {
                return BadRequest();
            }

            _context.Entry(order_History).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_HistoryExists(id))
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

        // POST: api/Order_History
        [HttpPost]
        public async Task<ActionResult<Order_History>> PostOrder_History(Order_History order_History)
        {
            _context.Order_History.Add(order_History);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_History", new { id = order_History.Id }, order_History);
        }

        // DELETE: api/Order_History/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order_History>> DeleteOrder_History(int id)
        {
            var order_History = await _context.Order_History.FindAsync(id);
            if (order_History == null)
            {
                return NotFound();
            }

            _context.Order_History.Remove(order_History);
            await _context.SaveChangesAsync();

            return order_History;
        }

        private bool Order_HistoryExists(int id)
        {
            return _context.Order_History.Any(e => e.Id == id);
        }
    }
}
