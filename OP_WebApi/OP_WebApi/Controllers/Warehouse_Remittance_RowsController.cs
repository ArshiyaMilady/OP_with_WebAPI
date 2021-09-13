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
    public class Warehouse_Remittance_RowsController : ControllerBase
    {
        private readonly TableContext _context;

        public Warehouse_Remittance_RowsController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Warehouse_Remittance_Rows?all=vvv&company_Id=xxx&WarehouseRemittance_Id=yyy&ItemSmallcode=yyy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse_Remittance_Row>>> GetWarehouse_Remittance_Row
            (string all = "yes", long company_Id = 0, long WarehouseRemittance_Id = 0, string ItemSmallcode = null)
        {
            if(all.Equals("yes"))
                return await _context.Warehouse_Remittance_Row.ToListAsync();
            else
            {
                List<Warehouse_Remittance_Row> rows = await _context.Warehouse_Remittance_Row
                    .Where(d => d.Company_Id == company_Id).ToListAsync();
                if (WarehouseRemittance_Id > 0)
                    rows = rows.Where(d => d.Warehouse_Remittance_Id == WarehouseRemittance_Id).ToList();
                if (!string.IsNullOrEmpty(ItemSmallcode))
                    rows = rows.Where(d => d.Item_SmallCode.ToLower().Equals(ItemSmallcode.ToLower())).ToList();
                return rows;
            }
        }

        // GET: api/Warehouse_Remittance_Rows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse_Remittance_Row>> GetWarehouse_Remittance_Row(long id)
        {
            var warehouse_Remittance_Row = await _context.Warehouse_Remittance_Row.FindAsync(id);

            if (warehouse_Remittance_Row == null)
            {
                return NotFound();
            }

            return warehouse_Remittance_Row;
        }

        // PUT: api/Warehouse_Remittance_Rows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse_Remittance_Row(long id, Warehouse_Remittance_Row warehouse_Remittance_Row)
        {
            if (id != warehouse_Remittance_Row.Id)
            {
                return BadRequest();
            }

            _context.Entry(warehouse_Remittance_Row).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Warehouse_Remittance_RowExists(id))
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

        // POST: api/Warehouse_Remittance_Rows
        [HttpPost]
        public async Task<ActionResult<Warehouse_Remittance_Row>> PostWarehouse_Remittance_Row(Warehouse_Remittance_Row warehouse_Remittance_Row)
        {
            _context.Warehouse_Remittance_Row.Add(warehouse_Remittance_Row);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse_Remittance_Row", new { id = warehouse_Remittance_Row.Id }, warehouse_Remittance_Row);
        }

        // DELETE: api/Warehouse_Remittance_Rows/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Warehouse_Remittance_Row>> DeleteWarehouse_Remittance_Row(long id)
        {
            var warehouse_Remittance_Row = await _context.Warehouse_Remittance_Row.FindAsync(id);
            if (warehouse_Remittance_Row == null)
            {
                return NotFound();
            }

            _context.Warehouse_Remittance_Row.Remove(warehouse_Remittance_Row);
            await _context.SaveChangesAsync();

            return warehouse_Remittance_Row;
        }

        private bool Warehouse_Remittance_RowExists(long id)
        {
            return _context.Warehouse_Remittance_Row.Any(e => e.Id == id);
        }
    }
}
