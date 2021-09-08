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
    public class CostCentersController : ControllerBase
    {
        private readonly TableContext _context;

        public CostCentersController(TableContext context)
        {
            _context = context;
        }

        // GET: api/CostCenters
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<CostCenter>>> GetCostCenter()
        {
            return await _context.CostCenter.ToListAsync();
        }

        // GET: api/CostCenters/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<CostCenter>> GetCostCenter(long id)
        {
            var costCenter = await _context.CostCenter.FindAsync(id);

            if (costCenter == null)
            {
                return NotFound();
            }

            return costCenter;
        }

        // PUT: api/CostCenters/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutCostCenter(long id, CostCenter costCenter)
        {
            if (id != costCenter.Id)
            {
                return BadRequest();
            }

            _context.Entry(costCenter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CostCenterExists(id))
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

        // POST: api/CostCenters
        [HttpPost, Authorize]
        public async Task<ActionResult<CostCenter>> PostCostCenter(CostCenter costCenter)
        {
            _context.CostCenter.Add(costCenter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCostCenter", new { id = costCenter.Id }, costCenter);
        }

        // DELETE: api/CostCenters/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<CostCenter>> DeleteCostCenter(long id)
        {
            var costCenter = await _context.CostCenter.FindAsync(id);
            if (costCenter == null)
            {
                return NotFound();
            }

            _context.CostCenter.Remove(costCenter);
            await _context.SaveChangesAsync();

            return costCenter;
        }

        private bool CostCenterExists(long id)
        {
            return _context.CostCenter.Any(e => e.Id == id);
        }
    }
}
