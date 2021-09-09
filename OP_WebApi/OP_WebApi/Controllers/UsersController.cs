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
    public class UsersController : ControllerBase
    {
        private readonly TableContext _context;

        public UsersController(TableContext context)
        {
            _context = context;


        }

        // GET: api/Users
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(string all="yes",long company_id=0)
        {
            if (all.Equals("yes"))
                return await _context.User.ToListAsync();
            else
                return await _context.User.Where(d => d.Company_Id == company_id).ToListAsync();
        }

        // GET: api/Users/5
        // GET: api/Users/0?name_mobile=xxxx&login_type=y&remove_password=0
        // login_type = 1 => name   /   login_type = 2 => mobile
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<User>> GetUser(long id,string name_mobile=null,int login_type=0,int remove_password=1)
        {
            User user = null;
            if (id > 0)
                user = await _context.User.FindAsync(id);
            else if (!string.IsNullOrEmpty(name_mobile))
            {
                if(login_type ==1)  // یافتن کاربر توسط نام کاربری
                    user = await _context.User.FirstOrDefaultAsync(d => d.Name.ToLower().Equals(name_mobile.ToLower()));
                else if (login_type == 2)   // یافتن کاربر توسط شماره همراه
                {
                    if (name_mobile.Length <= 10)
                        name_mobile = "0098" + name_mobile;
                    else if (name_mobile.Length == 11)
                        name_mobile = "0098" + name_mobile.Substring(name_mobile.Length-10);

                    user = await _context.User.FirstOrDefaultAsync(d => d.Mobile.Equals(name_mobile));
                }
            }

            if (user == null)
                return NotFound();
            else { if(remove_password==1) user.Password = null; }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost, Authorize]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetUser", new { id = user.Id }, user);
            return await _context.User.FirstOrDefaultAsync(d => d.Name.Equals(user.Name));
        }

        // DELETE: api/Users/5
        // DELETE: api/Users/0?company_id=xxx
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<User>> DeleteUser(long id,long company_id=0)
        {
            User user = null;
            if (company_id == 0)
            {
                user = await _context.User.FindAsync(id);
                if (user == null) return NotFound();

                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            // حذف تمام کاربران یک شرکت به غیر از ادمین واقعی
            else if ((id==0) && (company_id>0))
            {
                user = await _context.User.Where(d=>!d.Name.Equals("real_admin")).FirstAsync();
                if (user == null) return NotFound();
                _context.User.RemoveRange(_context.User.Where(d => !d.Name.Equals("real_admin")).ToList());
                await _context.SaveChangesAsync();
            }

            return user;
        }

        private bool UserExists(long id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
