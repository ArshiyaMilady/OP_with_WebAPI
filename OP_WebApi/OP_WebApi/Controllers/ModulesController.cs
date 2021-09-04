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
    public class ModulesController : ControllerBase
    {
        private readonly TableContext _context;

        public ModulesController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            return await _context.Module.ToListAsync();
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(long id)
        {
            var @module = await _context.Module.FindAsync(id);

            if (@module == null)
            {
                return NotFound();
            }

            return @module;
        }

        // PUT: api/Modules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(long id, Module @module)
        {
            if (id != @module.Id)
            {
                return BadRequest();
            }

            _context.Entry(@module).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
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

        // POST: api/Modules
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            _context.Module.Add(@module);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Module>> DeleteModule(long id)
        {
            var @module = await _context.Module.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            _context.Module.Remove(@module);
            await _context.SaveChangesAsync();

            return @module;
        }

        private bool ModuleExists(long id)
        {
            return _context.Module.Any(e => e.Id == id);
        }
    }
}
