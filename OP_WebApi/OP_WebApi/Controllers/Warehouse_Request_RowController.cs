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
    public class Warehouse_Request_RowController : ControllerBase
    {
        private readonly TableContext _context;

        public Warehouse_Request_RowController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Warehouse_Request_Row
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Warehouse_Request_Row>>> GetWarehouse_Request_Row()
        {
            return await _context.Warehouse_Request_Row.ToListAsync();
        }

        // GET: api/Warehouse_Request_Row/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Warehouse_Request_Row>> GetWarehouse_Request_Row(long id)
        {
            var warehouse_Request_Row = await _context.Warehouse_Request_Row.FindAsync(id);

            if (warehouse_Request_Row == null)
            {
                return NotFound();
            }

            return warehouse_Request_Row;
        }

        // PUT: api/Warehouse_Request_Row/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutWarehouse_Request_Row(long id, Warehouse_Request_Row warehouse_Request_Row)
        {
            if (id != warehouse_Request_Row.Id)
            {
                return BadRequest();
            }

            _context.Entry(warehouse_Request_Row).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Warehouse_Request_RowExists(id))
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

        // POST: api/Warehouse_Request_Row
        [HttpPost, Authorize]
        public async Task<ActionResult<Warehouse_Request_Row>> PostWarehouse_Request_Row(Warehouse_Request_Row warehouse_Request_Row)
        {
            _context.Warehouse_Request_Row.Add(warehouse_Request_Row);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse_Request_Row", new { id = warehouse_Request_Row.Id }, warehouse_Request_Row);
        }

        // DELETE: api/Warehouse_Request_Row/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Warehouse_Request_Row>> DeleteWarehouse_Request_Row(long id)
        {
            var warehouse_Request_Row = await _context.Warehouse_Request_Row.FindAsync(id);
            if (warehouse_Request_Row == null)
            {
                return NotFound();
            }

            _context.Warehouse_Request_Row.Remove(warehouse_Request_Row);
            await _context.SaveChangesAsync();

            return warehouse_Request_Row;
        }

        private bool Warehouse_Request_RowExists(long id)
        {
            return _context.Warehouse_Request_Row.Any(e => e.Id == id);
        }
    }
}
