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
    public class OL_ULController : ControllerBase
    {
        private readonly TableContext _context;

        public OL_ULController(TableContext context)
        {
            _context = context;
        }

        // GET: api/OL_UL
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<OL_UL>>> GetOL_UL()
        {
            return await _context.OL_UL.ToListAsync();
        }

        // GET: api/OL_UL/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<OL_UL>> GetOL_UL(long id)
        {
            var oL_UL = await _context.OL_UL.FindAsync(id);

            if (oL_UL == null)
            {
                return NotFound();
            }

            return oL_UL;
        }

        // PUT: api/OL_UL/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutOL_UL(long id, OL_UL oL_UL)
        {
            if (id != oL_UL.Id)
            {
                return BadRequest();
            }

            _context.Entry(oL_UL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OL_ULExists(id))
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

        // POST: api/OL_UL
        [HttpPost, Authorize]
        public async Task<ActionResult<OL_UL>> PostOL_UL(OL_UL oL_UL)
        {
            _context.OL_UL.Add(oL_UL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOL_UL", new { id = oL_UL.Id }, oL_UL);
        }

        // DELETE: api/OL_UL/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<OL_UL>> DeleteOL_UL(long id)
        {
            var oL_UL = await _context.OL_UL.FindAsync(id);
            if (oL_UL == null)
            {
                return NotFound();
            }

            _context.OL_UL.Remove(oL_UL);
            await _context.SaveChangesAsync();

            return oL_UL;
        }

        private bool OL_ULExists(long id)
        {
            return _context.OL_UL.Any(e => e.Id == id);
        }
    }
}
