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
    public class Order_CustomerController : ControllerBase
    {
        private readonly TableContext _context;

        public Order_CustomerController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Order_Customer
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Order_Customer>>> GetOrder_Customer()
        {
            return await _context.Order_Customer.ToListAsync();
        }

        // GET: api/Order_Customer/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Order_Customer>> GetOrder_Customer(long id)
        {
            var order_Customer = await _context.Order_Customer.FindAsync(id);

            if (order_Customer == null)
            {
                return NotFound();
            }

            return order_Customer;
        }

        // PUT: api/Order_Customer/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutOrder_Customer(long id, Order_Customer order_Customer)
        {
            if (id != order_Customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(order_Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_CustomerExists(id))
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

        // POST: api/Order_Customer
        [HttpPost, Authorize]
        public async Task<ActionResult<Order_Customer>> PostOrder_Customer(Order_Customer order_Customer)
        {
            _context.Order_Customer.Add(order_Customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder_Customer", new { id = order_Customer.Id }, order_Customer);
        }

        // DELETE: api/Order_Customer/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Order_Customer>> DeleteOrder_Customer(long id)
        {
            var order_Customer = await _context.Order_Customer.FindAsync(id);
            if (order_Customer == null)
            {
                return NotFound();
            }

            _context.Order_Customer.Remove(order_Customer);
            await _context.SaveChangesAsync();

            return order_Customer;
        }

        private bool Order_CustomerExists(long id)
        {
            return _context.Order_Customer.Any(e => e.Id == id);
        }
    }
}
