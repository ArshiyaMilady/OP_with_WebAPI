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
    public class UL_See_OLController : ControllerBase
    {
        private readonly TableContext _context;

        public UL_See_OLController(TableContext context)
        {
            _context = context;
        }

        // GET: api/UL_See_OL?all=vvv&company_Id=xxx&ul_Id=zzz
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UL_See_OL>>> GetUL_See_OL
            (string all = "yes", long company_Id = 0, long ul_Id = -1)
        {
            if(all.Equals("yes"))
                return await _context.UL_See_OL.ToListAsync();
            else
            {
                if (ul_Id > 0)
                    return await _context.UL_See_OL.Where(d => d.Company_Id == company_Id)
                        .Where(j => j.UL_Id == ul_Id).ToListAsync();
                else
                    return await _context.UL_See_OL.Where(d => d.Company_Id == company_Id).ToListAsync();
            }
        }

        // GET: api/UL_See_OL/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<UL_See_OL>> GetUL_See_OL(long id)
        {
            var uL_See_OL = await _context.UL_See_OL.FindAsync(id);

            if (uL_See_OL == null)
            {
                return NotFound();
            }

            return uL_See_OL;
        }

        // PUT: api/UL_See_OL/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUL_See_OL(long id, UL_See_OL uL_See_OL)
        {
            if (id != uL_See_OL.Id)
            {
                return BadRequest();
            }

            _context.Entry(uL_See_OL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UL_See_OLExists(id))
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

        // POST: api/UL_See_OL
        [HttpPost, Authorize]
        public async Task<ActionResult<UL_See_OL>> PostUL_See_OL(UL_See_OL uL_See_OL)
        {
            _context.UL_See_OL.Add(uL_See_OL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUL_See_OL", new { id = uL_See_OL.Id }, uL_See_OL);
        }

        // DELETE: api/UL_See_OL/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<UL_See_OL>> DeleteUL_See_OL(long id)
        {
            var uL_See_OL = await _context.UL_See_OL.FindAsync(id);
            if (uL_See_OL == null)
            {
                return NotFound();
            }

            _context.UL_See_OL.Remove(uL_See_OL);
            await _context.SaveChangesAsync();

            return uL_See_OL;
        }

        private bool UL_See_OLExists(long id)
        {
            return _context.UL_See_OL.Any(e => e.Id == id);
        }
    }
}
