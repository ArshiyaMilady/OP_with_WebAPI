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
    public class User_Level_UL_FeatureController : ControllerBase
    {
        private readonly TableContext _context;

        public User_Level_UL_FeatureController(TableContext context)
        {
            _context = context;
        }

        // GET: api/User_Level_UL_Feature
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User_Level_UL_Feature>>> GetLevel_UL_Feature()
        {
            return await _context.User_Level_UL_Feature.ToListAsync();
        }

        // GET: api/User_Level_UL_Feature/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User_Level_UL_Feature>> GetUser_Level_UL_Feature(long id)
        {
            var user_Level_UL_Feature = await _context.User_Level_UL_Feature.FindAsync(id);

            if (user_Level_UL_Feature == null)
            {
                return NotFound();
            }

            return user_Level_UL_Feature;
        }

        // PUT: api/User_Level_UL_Feature/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUser_Level_UL_Feature(long id, User_Level_UL_Feature user_Level_UL_Feature)
        {
            if (id != user_Level_UL_Feature.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_Level_UL_Feature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_Level_UL_FeatureExists(id))
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

        // POST: api/User_Level_UL_Feature
        [HttpPost, Authorize]
        public async Task<ActionResult<User_Level_UL_Feature>> PostUser_Level_UL_Feature(User_Level_UL_Feature user_Level_UL_Feature)
        {
            _context.User_Level_UL_Feature.Add(user_Level_UL_Feature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Level_UL_Feature", new { id = user_Level_UL_Feature.Id }, user_Level_UL_Feature);
        }

        // DELETE: api/User_Level_UL_Feature/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<User_Level_UL_Feature>> DeleteUser_Level_UL_Feature(long id)
        {
            var user_Level_UL_Feature = await _context.User_Level_UL_Feature.FindAsync(id);
            if (user_Level_UL_Feature == null)
            {
                return NotFound();
            }

            _context.User_Level_UL_Feature.Remove(user_Level_UL_Feature);
            await _context.SaveChangesAsync();

            return user_Level_UL_Feature;
        }

        private bool User_Level_UL_FeatureExists(long id)
        {
            return _context.User_Level_UL_Feature.Any(e => e.Id == id);
        }
    }
}
