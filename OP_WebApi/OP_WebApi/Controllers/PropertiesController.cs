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
    public class PropertiesController : ControllerBase
    {
        private readonly TableContext _context;

        public PropertiesController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Properties
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperty()
        {
            return await _context.Property.ToListAsync();
        }

        // GET: api/Properties/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Property>> GetProperty(long id)
        {
            var @property = await _context.Property.FindAsync(id);

            if (@property == null)
            {
                return NotFound();
            }

            return @property;
        }

        // PUT: api/Properties/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutProperty(long id, Property @property)
        {
            if (id != @property.Id)
            {
                return BadRequest();
            }

            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST: api/Properties
        [HttpPost, Authorize]
        public async Task<ActionResult<Property>> PostProperty(Property @property)
        {
            _context.Property.Add(@property);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProperty", new { id = @property.Id }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Property>> DeleteProperty(long id)
        {
            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Property.Remove(@property);
            await _context.SaveChangesAsync();

            return @property;
        }

        private bool PropertyExists(long id)
        {
            return _context.Property.Any(e => e.Id == id);
        }
    }
}
