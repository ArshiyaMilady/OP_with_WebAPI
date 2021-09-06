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
    public class User_ULController : ControllerBase
    {
        private readonly TableContext _context;

        public User_ULController(TableContext context)
        {
            _context = context;
        }

        // GET: api/User_UL
        // GET: api/User_UL?type=vvv&user_id=xxx
        [HttpGet, Authorize]
        // نوع داده شناسه را رشته قرار دادم که با تابع بعدی اشت
        public async Task<ActionResult<IEnumerable<User_UL>>> GetUser_UL(string type = "all", string user_id=null)
        {
            if (type.Equals("all"))
                return await _context.User_UL.ToListAsync();
            else //if (string.IsNullOrEmpty(user_id))
                return await _context.User_UL.Where(d => d.User_Id == Convert.ToInt64(user_id)).ToListAsync();
        }

        // GET: api/User_UL/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User_UL>> GetUser_UL(long id)
        {
            User_UL user_UL = await _context.User_UL.FindAsync(id);

            if (user_UL == null)
                return NotFound();

            return user_UL;
        }

        // PUT: api/User_UL/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUser_UL(long id, User_UL user_UL)
        {
            if (id != user_UL.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_UL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_ULExists(id))
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

        // POST: api/User_UL
        [HttpPost, Authorize]
        public async Task<ActionResult<User_UL>> PostUser_UL(User_UL user_UL)
        {
            _context.User_UL.Add(user_UL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_UL", new { id = user_UL.Id }, user_UL);
        }

        // DELETE: api/User_UL/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<User_UL>> DeleteUser_UL(long id)
        {
            var user_UL = await _context.User_UL.FindAsync(id);
            if (user_UL == null)
            {
                return NotFound();
            }

            _context.User_UL.Remove(user_UL);
            await _context.SaveChangesAsync();

            return user_UL;
        }

        private bool User_ULExists(long id)
        {
            return _context.User_UL.Any(e => e.Id == id);
        }
    }
}
