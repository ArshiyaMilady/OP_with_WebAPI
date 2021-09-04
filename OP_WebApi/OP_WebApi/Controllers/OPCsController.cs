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
    public class OPCsController : ControllerBase
    {
        private readonly TableContext _context;

        public OPCsController(TableContext context)
        {
            _context = context;
        }

        // GET: api/OPCs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OPC>>> GetOPC()
        {
            return await _context.OPC.ToListAsync();
        }

        // GET: api/OPCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OPC>> GetOPC(long id)
        {
            var oPC = await _context.OPC.FindAsync(id);

            if (oPC == null)
            {
                return NotFound();
            }

            return oPC;
        }

        // PUT: api/OPCs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOPC(long id, OPC oPC)
        {
            if (id != oPC.Id)
            {
                return BadRequest();
            }

            _context.Entry(oPC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OPCExists(id))
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

        // POST: api/OPCs
        [HttpPost]
        public async Task<ActionResult<OPC>> PostOPC(OPC oPC)
        {
            _context.OPC.Add(oPC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOPC", new { id = oPC.Id }, oPC);
        }

        // DELETE: api/OPCs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OPC>> DeleteOPC(long id)
        {
            var oPC = await _context.OPC.FindAsync(id);
            if (oPC == null)
            {
                return NotFound();
            }

            _context.OPC.Remove(oPC);
            await _context.SaveChangesAsync();

            return oPC;
        }

        private bool OPCExists(long id)
        {
            return _context.OPC.Any(e => e.Id == id);
        }
    }
}
