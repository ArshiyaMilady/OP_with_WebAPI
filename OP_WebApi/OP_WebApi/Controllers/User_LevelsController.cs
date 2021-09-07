﻿using System;
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
    public class User_LevelsController : ControllerBase
    {
        private readonly TableContext _context;

        public User_LevelsController(TableContext context)
        {
            _context = context;
        }

        // GET: api/User_Levels
        // GET: api/User_UL?all=vvv&user_id=xxx
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User_Level>>> GetUser_Level(string all="yes",long company_id=0)
        {
            if(all.Equals("all"))
                return await _context.User_Level.ToListAsync();
            else
                return await _context.User_Level.Where(d=>d.Company_Id == company_id).ToListAsync();
        }

        // GET: api/User_Levels/5
        // GET: api/User_Levels/0?user_id=xxx
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User_Level>> GetUser_Level(long id,long user_id=0)
        {
            User_Level user_Level = null;
            if (user_id > 0)
            {
                if (await _context.User_UL.AnyAsync(d => d.User_Id == user_id))
                {
                    long ul_id = (await _context.User_UL.FirstOrDefaultAsync(d => d.User_Id == user_id)).Id;
                    user_Level = await _context.User_Level.FirstOrDefaultAsync(d=>d.Id == ul_id);
                }
            }
            else
                user_Level = await _context.User_Level.FindAsync(id);

            if (user_Level == null)
            {
                return NotFound();
            }

            return user_Level;
        }

        // PUT: api/User_Levels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_Level(long id, User_Level user_Level)
        {
            if (id != user_Level.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_Level).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_LevelExists(id))
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

        // POST: api/User_Levels
        [HttpPost]
        public async Task<ActionResult<User_Level>> PostUser_Level(User_Level user_Level)
        {
            _context.User_Level.Add(user_Level);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Level", new { id = user_Level.Id }, user_Level);
        }

        // DELETE: api/User_Levels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User_Level>> DeleteUser_Level(long id)
        {
            var user_Level = await _context.User_Level.FindAsync(id);
            if (user_Level == null)
            {
                return NotFound();
            }

            _context.User_Level.Remove(user_Level);
            await _context.SaveChangesAsync();

            return user_Level;
        }

        private bool User_LevelExists(long id)
        {
            return _context.User_Level.Any(e => e.Id == id);
        }
    }
}
