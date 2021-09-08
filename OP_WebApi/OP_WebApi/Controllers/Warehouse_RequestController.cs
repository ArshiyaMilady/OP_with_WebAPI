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
    public class Warehouse_RequestController : ControllerBase
    {
        private readonly TableContext _context;

        public Warehouse_RequestController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Warehouse_Request
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Warehouse_Request>>> GetWarehouse_Request()
        {
            return await _context.Warehouse_Request.ToListAsync();
        }

        // GET: api/Warehouse_Request/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Warehouse_Request>> GetWarehouse_Request(long id)
        {
            var warehouse_Request = await _context.Warehouse_Request.FindAsync(id);

            if (warehouse_Request == null)
            {
                return NotFound();
            }

            return warehouse_Request;
        }

        // PUT: api/Warehouse_Request/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutWarehouse_Request(long id, Warehouse_Request warehouse_Request)
        {
            if (id != warehouse_Request.Id)
            {
                return BadRequest();
            }

            _context.Entry(warehouse_Request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Warehouse_RequestExists(id))
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

        // POST: api/Warehouse_Request
        [HttpPost, Authorize]
        public async Task<ActionResult<Warehouse_Request>> PostWarehouse_Request(Warehouse_Request warehouse_Request)
        {
            _context.Warehouse_Request.Add(warehouse_Request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse_Request", new { id = warehouse_Request.Id }, warehouse_Request);
        }

        // DELETE: api/Warehouse_Request/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Warehouse_Request>> DeleteWarehouse_Request(long id)
        {
            var warehouse_Request = await _context.Warehouse_Request.FindAsync(id);
            if (warehouse_Request == null)
            {
                return NotFound();
            }

            _context.Warehouse_Request.Remove(warehouse_Request);
            await _context.SaveChangesAsync();

            return warehouse_Request;
        }

        private bool Warehouse_RequestExists(long id)
        {
            return _context.Warehouse_Request.Any(e => e.Id == id);
        }
    }
}
