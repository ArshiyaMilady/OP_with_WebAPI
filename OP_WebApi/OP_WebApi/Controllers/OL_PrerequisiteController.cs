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
    public class OL_PrerequisiteController : ControllerBase
    {
        private readonly TableContext _context;

        public OL_PrerequisiteController(TableContext context)
        {
            _context = context;
        }

        // GET: api/OL_Prerequisite
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<OL_Prerequisite>>> GetOL_Prerequisite()
        {
            return await _context.OL_Prerequisite.ToListAsync();
        }

        // GET: api/OL_Prerequisite/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<OL_Prerequisite>> GetOL_Prerequisite(long id)
        {
            var oL_Prerequisite = await _context.OL_Prerequisite.FindAsync(id);

            if (oL_Prerequisite == null)
            {
                return NotFound();
            }

            return oL_Prerequisite;
        }

        // PUT: api/OL_Prerequisite/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutOL_Prerequisite(long id, OL_Prerequisite oL_Prerequisite)
        {
            if (id != oL_Prerequisite.Id)
            {
                return BadRequest();
            }

            _context.Entry(oL_Prerequisite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OL_PrerequisiteExists(id))
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

        // POST: api/OL_Prerequisite
        [HttpPost, Authorize]
        public async Task<ActionResult<OL_Prerequisite>> PostOL_Prerequisite(OL_Prerequisite oL_Prerequisite)
        {
            _context.OL_Prerequisite.Add(oL_Prerequisite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOL_Prerequisite", new { id = oL_Prerequisite.Id }, oL_Prerequisite);
        }

        // DELETE: api/OL_Prerequisite/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<OL_Prerequisite>> DeleteOL_Prerequisite(long id)
        {
            var oL_Prerequisite = await _context.OL_Prerequisite.FindAsync(id);
            if (oL_Prerequisite == null)
            {
                return NotFound();
            }

            _context.OL_Prerequisite.Remove(oL_Prerequisite);
            await _context.SaveChangesAsync();

            return oL_Prerequisite;
        }

        private bool OL_PrerequisiteExists(long id)
        {
            return _context.OL_Prerequisite.Any(e => e.Id == id);
        }
    }
}
