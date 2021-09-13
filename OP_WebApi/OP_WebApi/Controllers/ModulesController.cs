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
    public class ModulesController : ControllerBase
    {
        private readonly TableContext _context;

        public ModulesController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Modules?all=vvv&company_Id=xxx&EnableType=yyy&ModuleSmallCode=zzz
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule
            (string all = "yes", long company_Id = 0, int EnableType = 0, string ModuleSmallCode = null)
        {
            if(all.Equals("yes"))
                return await _context.Module.ToListAsync();
            else
            {
                List<Module> modules = await _context.Module.Where(d=>d.Company_Id==company_Id).ToListAsync();
                if (EnableType == 1) modules = modules.Where(d => d.Enable).ToList();
                else if (EnableType == -1) modules = modules.Where(d => !d.Enable).ToList();

                if (!string.IsNullOrEmpty(ModuleSmallCode))
                    modules = modules.Where(d => d.Module_Code_Small.ToLower().Equals(ModuleSmallCode.ToLower())).ToList();

                return modules;
            }
        }

        // GET: api/Modules/5
        [HttpGet("{id}"), Authorize]
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
        [HttpPut("{id}"), Authorize]
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
        [HttpPost, Authorize]
        public async Task<ActionResult<Module>> PostModule(Module @module)
        {
            _context.Module.Add(@module);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}"), Authorize]
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
