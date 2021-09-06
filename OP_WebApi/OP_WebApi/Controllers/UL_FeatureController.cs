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
    public class UL_FeatureController : ControllerBase
    {
        private readonly TableContext _context;

        public UL_FeatureController(TableContext context)
        {
            _context = context;
        }

        // GET: api/UL_Feature?company_Id=xxx&EnableType=yyy&ul_Id=zzz
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UL_Feature>>> GetUL_Feature(long company_Id,int EnableType,long ul_Id=-1)
        {
            if(company_Id == 0)
                return await _context.UL_Feature.ToListAsync();
            else
            {
                List<UL_Feature> lstULF = await _context.UL_Feature.Where(d=>d.Company_Id == company_Id).ToListAsync();
                if (EnableType == 1)
                    lstULF = lstULF.Where(d => d.Enabled).ToList();
                else if (EnableType == -1)
                    lstULF=lstULF.Where(d => !d.Enabled).ToList();

                // بنا به سطح کاربر، امکانات را مشخص کرده و بر می گرداند
                #region دسترسی های کاربر با توجه به سطح کاربری
                if (ul_Id>=0)
                {
                    int ul_type = _context.User_Level.FirstOrDefault(d => d.Id == ul_Id).Type;
                    if (ul_type != 1)
                    {
                        if (ul_type == 2)
                        {
                            // تمام امکانات به غیر از امکانات ادمین واقعی
                            lstULF = lstULF.Where(d => !d.Unique_Phrase.Substring(0, 1).Equals("d")).ToList();
                        }
                        else
                        {
                            List<User_Level_UL_Feature> lstULULF = await _context.User_Level_UL_Feature
                                .Where(d => d.User_Level_Id == ul_Id).ToListAsync();
                            lstULF = lstULF.Where(d => lstULULF.Any(j => j.UL_Feature_Id == d.Id)).ToList();
                        }
                    }
                }
                #endregion

                return lstULF;
            }
        }

        // GET: api/UL_Feature/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<UL_Feature>> GetUL_Feature(long id)
        {
            var uL_Feature = await _context.UL_Feature.FindAsync(id);

            if (uL_Feature == null)
            {
                return NotFound();
            }

            return uL_Feature;
        }

        // PUT: api/UL_Feature/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUL_Feature(long id, UL_Feature uL_Feature)
        {
            if (id != uL_Feature.Id)
            {
                return BadRequest();
            }

            _context.Entry(uL_Feature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UL_FeatureExists(id))
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

        // POST: api/UL_Feature
        [HttpPost]
        public async Task<ActionResult<UL_Feature>> PostUL_Feature(UL_Feature uL_Feature)
        {
            _context.UL_Feature.Add(uL_Feature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUL_Feature", new { id = uL_Feature.Id }, uL_Feature);
        }

        // DELETE: api/UL_Feature/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UL_Feature>> DeleteUL_Feature(long id)
        {
            var uL_Feature = await _context.UL_Feature.FindAsync(id);
            if (uL_Feature == null)
            {
                return NotFound();
            }

            _context.UL_Feature.Remove(uL_Feature);
            await _context.SaveChangesAsync();

            return uL_Feature;
        }

        private bool UL_FeatureExists(long id)
        {
            return _context.UL_Feature.Any(e => e.Id == id);
        }
    }
}
