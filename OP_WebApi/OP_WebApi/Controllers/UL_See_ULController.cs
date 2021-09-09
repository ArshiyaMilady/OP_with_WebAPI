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
    public class UL_See_ULController : ControllerBase
    {
        private readonly TableContext _context;

        public UL_See_ULController(TableContext context)
        {
            _context = context;
        }

        // GET: api/UL_See_UL?all=vvv&company_id=xxx&main_ul_id=yyy
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UL_See_UL>>> GetUL_See_UL(string all="yes", long company_id=0,long main_ul_id=0)
        {
            if (all.Equals("yes"))
                return await _context.UL_See_UL.ToListAsync();
            else
            {
                if (main_ul_id == 0)
                    return await _context.UL_See_UL.Where(d => d.Company_Id == company_id).ToListAsync();
                else return await _context.UL_See_UL.Where(d => d.Company_Id == company_id)
                        .Where(j => j.MainUL_Id == main_ul_id).ToListAsync();
            }
        }

        // GET: api/UL_See_UL/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<UL_See_UL>> GetUL_See_UL(long id)
        {
            var uL_See_UL = await _context.UL_See_UL.FindAsync(id);

            if (uL_See_UL == null)
            {
                return NotFound();
            }

            return uL_See_UL;
        }

        // PUT: api/UL_See_UL/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUL_See_UL(long id, UL_See_UL uL_See_UL)
        {
            if (id != uL_See_UL.Id)
            {
                return BadRequest();
            }

            _context.Entry(uL_See_UL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UL_See_ULExists(id))
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

        // POST: api/UL_See_UL
        [HttpPost, Authorize]
        public async Task<ActionResult<UL_See_UL>> PostUL_See_UL(UL_See_UL uL_See_UL)
        {
            _context.UL_See_UL.Add(uL_See_UL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUL_See_UL", new { id = uL_See_UL.Id }, uL_See_UL);
        }

        // DELETE: api/UL_See_UL/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<UL_See_UL>> DeleteUL_See_UL(long id)
        {
            var uL_See_UL = await _context.UL_See_UL.FindAsync(id);
            if (uL_See_UL == null)
            {
                return NotFound();
            }

            _context.UL_See_UL.Remove(uL_See_UL);
            await _context.SaveChangesAsync();

            return uL_See_UL;
        }

        private bool UL_See_ULExists(long id)
        {
            return _context.UL_See_UL.Any(e => e.Id == id);
        }
    }
}
