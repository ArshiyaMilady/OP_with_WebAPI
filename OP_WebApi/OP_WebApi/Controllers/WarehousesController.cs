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
    public class WarehousesController : ControllerBase
    {
        private readonly TableContext _context;

        public WarehousesController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Warehouses?all=vvv&company_Id=xxx
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouse
            (string all = "yes", long company_Id = 0)
        {
            if(all.Equals("yes")) return await _context.Warehouse.ToListAsync();
            else return await _context.Warehouse.Where(d=>d.Company_Id==company_Id).ToListAsync();
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Warehouse>> GetWarehouse(long id)
        {
            var warehouse = await _context.Warehouse.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // PUT: api/Warehouses/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutWarehouse(long id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        // POST: api/Warehouses
        [HttpPost, Authorize]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
            _context.Warehouse.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.Id }, warehouse);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Warehouse>> DeleteWarehouse(long id)
        {
            var warehouse = await _context.Warehouse.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();

            return warehouse;
        }

        private bool WarehouseExists(long id)
        {
            return _context.Warehouse.Any(e => e.Id == id);
        }
    }
}
