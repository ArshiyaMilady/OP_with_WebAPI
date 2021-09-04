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
    public class LoginHistoriesController : ControllerBase
    {
        private readonly TableContext _context;

        public LoginHistoriesController(TableContext context)
        {
            _context = context;
        }

        // GET: api/LoginHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginHistory>>> GetLoginHistory()
        {
            return await _context.LoginHistory.ToListAsync();
        }

        // GET: api/LoginHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginHistory>> GetLoginHistory(long id)
        {
            var loginHistory = await _context.LoginHistory.FindAsync(id);

            if (loginHistory == null)
            {
                return NotFound();
            }

            return loginHistory;
        }

        // PUT: api/LoginHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginHistory(long id, LoginHistory loginHistory)
        {
            if (id != loginHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(loginHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginHistoryExists(id))
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

        // POST: api/LoginHistories
        [HttpPost]
        public async Task<ActionResult<LoginHistory>> PostLoginHistory(LoginHistory loginHistory)
        {
            _context.LoginHistory.Add(loginHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoginHistory", new { id = loginHistory.Id }, loginHistory);
        }

        // DELETE: api/LoginHistories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LoginHistory>> DeleteLoginHistory(long id)
        {
            var loginHistory = await _context.LoginHistory.FindAsync(id);
            if (loginHistory == null)
            {
                return NotFound();
            }

            _context.LoginHistory.Remove(loginHistory);
            await _context.SaveChangesAsync();

            return loginHistory;
        }

        private bool LoginHistoryExists(long id)
        {
            return _context.LoginHistory.Any(e => e.Id == id);
        }
    }
}
