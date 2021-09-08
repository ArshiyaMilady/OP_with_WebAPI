//using System;
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
    public class ActionsController : ControllerBase
    {
        private readonly TableContext _context;

        public ActionsController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Actions
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Action>>> GetAction()
        {
            return await _context.Action.ToListAsync();
        }

        // GET: api/Actions/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Action>> GetAction(long id)
        {
            var action = await _context.Action.FindAsync(id);

            if (action == null)
            {
                return NotFound();
            }

            return action;
        }

        // PUT: api/Actions/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutAction(long id, Action action)
        {
            if (id != action.Id)
            {
                return BadRequest();
            }

            _context.Entry(action).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionExists(id))
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

        // POST: api/Actions
        [HttpPost, Authorize]
        public async Task<ActionResult<Action>> PostAction(Action action)
        {
            _context.Action.Add(action);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAction", new { id = action.Id }, action);
        }

        // DELETE: api/Actions/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Action>> DeleteAction(long id)
        {
            var action = await _context.Action.FindAsync(id);
            if (action == null)
            {
                return NotFound();
            }

            _context.Action.Remove(action);
            await _context.SaveChangesAsync();

            return action;
        }

        private bool ActionExists(long id)
        {
            return _context.Action.Any(e => e.Id == id);
        }
    }
}
