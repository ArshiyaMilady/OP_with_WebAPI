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
    public class UL_Request_CategoryController : ControllerBase
    {
        private readonly TableContext _context;

        public UL_Request_CategoryController(TableContext context)
        {
            _context = context;
        }

        // GET: api/UL_Request_Category?all=vvv&company_Id=xxx
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UL_Request_Category>>> GetUL_Request_Category
            (string all = "yes", long company_Id = 0)
        {
            if (all.Equals("yes"))
                return await _context.UL_Request_Category.ToListAsync();
            else return await _context.UL_Request_Category.Where(d=>d.Company_Id == company_Id).ToListAsync();
        }

        // GET: api/UL_Request_Category/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<UL_Request_Category>> GetUL_Request_Category(long id)
        {
            var uL_Request_Category = await _context.UL_Request_Category.FindAsync(id);

            if (uL_Request_Category == null)
            {
                return NotFound();
            }

            return uL_Request_Category;
        }

        // PUT: api/UL_Request_Category/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUL_Request_Category(long id, UL_Request_Category uL_Request_Category)
        {
            if (id != uL_Request_Category.Id)
            {
                return BadRequest();
            }

            _context.Entry(uL_Request_Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UL_Request_CategoryExists(id))
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

        // POST: api/UL_Request_Category
        [HttpPost, Authorize]
        public async Task<ActionResult<UL_Request_Category>> PostUL_Request_Category(UL_Request_Category uL_Request_Category)
        {
            _context.UL_Request_Category.Add(uL_Request_Category);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUL_Request_Category", new { id = uL_Request_Category.Id }, uL_Request_Category);
            return uL_Request_Category;
        }

        // DELETE: api/UL_Request_Category/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<UL_Request_Category>> DeleteUL_Request_Category(long id)
        {
            var uL_Request_Category = await _context.UL_Request_Category.FindAsync(id);
            if (uL_Request_Category == null)
            {
                return NotFound();
            }

            _context.UL_Request_Category.Remove(uL_Request_Category);
            await _context.SaveChangesAsync();

            return uL_Request_Category;
        }

        private bool UL_Request_CategoryExists(long id)
        {
            return _context.UL_Request_Category.Any(e => e.Id == id);
        }
    }
}
