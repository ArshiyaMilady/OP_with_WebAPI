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
    public class CompaniesController : ControllerBase
    {
        //TableContext _context = new TableContext();
        private readonly TableContext _context;

        public CompaniesController(TableContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany()
        {
            //if (!_context.User.Any())
            //{
            //    SetCompanies();
            //    CreateUser_RealAdmin();
            //    SetUserLevels();
            //    SetUsers();
            //    SetOrderLevels();
            //    SetWareHouses();
            //}

            return await _context.Company.ToListAsync();
        }

        // GET: api/Companies/5
        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<Company>> GetCompany(long id)
        {
            var company = await _context.Company.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutCompany(long id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        [HttpPost, Authorize]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Company.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<Company>> DeleteCompany(long id)
        {
            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Company.Remove(company);
            await _context.SaveChangesAsync();

            return company;
        }

        private bool CompanyExists(long id)
        {
            return _context.Company.Any(e => e.Id == id);
        }

        // تعریف شرکت ها
        public void SetCompanies()
        {
            if (!_context.Company.Any())
            {
                _context.Company.Add(new Company
                {
                    Real_Name = "داریا نگاره هوشمند",
                    Active = true,
                    DateTime_mi = DateTime.Now,
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                });
                _context.SaveChanges();
            }
        }

        public void CreateUser_RealAdmin()
        {
            #region ساختن سطح کاربری ادمین واقعی
            if (!_context.User_Level.Any(d => d.Type == 1))
            {
                _context.User_Level.Add(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "real admin",
                    Enabled = true,
                    Type = 1,
                });      _context.SaveChanges();
            }
            #endregion

            #region ساختن کاربر ادمین واقعی
            CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();

            // ادمین
            string user_name = "real_admin";
            if (!_context.User.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("9999", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "Master",
                    Active = true,
                    //User_Level = Stack.UserLevel_Admin,
                    UserLevel_Description = "ادمین واقعی",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                });      _context.SaveChanges();
            }
            #endregion

            #region ساختن رابطه کاربر ادمین واقعی با سطح آن
            long user_id = _context.User.First(d => d.Name.Equals(user_name)).Id;
            if (!_context.User_UL.Where(d => d.Company_Id == Stack.Company_Id).Any(j => j.User_Id == user_id))
            {
                Models.User_Level ul = _context.User_Level.Where(d => d.Company_Id == Stack.Company_Id).FirstOrDefault(d => d.Type == 1);
                _context.User_UL.Add(new Models.User_UL
                {
                    Company_Id = Stack.Company_Id,
                    User_Id = user_id,
                    UL_Id = ul.Id,
                    UL_Description = ul.Description,
                });      _context.SaveChanges();
            }

            #endregion
        }

        public void SetUserLevels()
        {
            if (!_context.User_Level.Where(d => d.Company_Id == Stack.Company_Id).Any(d => d.Type == 2))
            {
                _context.User_Level.Add(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "admin",
                    Enabled = true,
                    Type = 2,
                });      _context.SaveChanges();
            }

            if (!_context.User_Level.Where(d => d.Company_Id == Stack.Company_Id).Any(d => d.Type == 3))
            {
                _context.User_Level.Add(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "کاربر ارشد اتوماتیک",
                    Enabled = true,
                    Type = 3,
                });      _context.SaveChanges();

                _context.User_Level.Add(new Models.User_Level
                {
                    Company_Id = Stack.Company_Id,
                    Description = "کاربر ارشد",
                    Enabled = true,
                    Type = 3,
                });      _context.SaveChanges();
            }
        }

        public void SetOrderLevels()
        {
            List<Models.Order_Level> lstOL = _context.Order_Level.Where(d => d.Company_Id == Stack.Company_Id).ToList();
            //if (!lstOL.Any()) return;   // اگر جدول خالی بود، اطلاعات زیر را اضافه کن

            int sequence = 0;

            // به طور کامل حذف شده است
            sequence = -1000;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    RemovingLevel = true,
                    Description = "حذف سفارش", // "به طور کامل حذف شده است",
                    Enabled = true,
                    Type = 0,
                    Description2 = "حذف شده است"
                });      _context.SaveChanges();
            }

            //سفارش کنسل شده است
            sequence = -100;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    CancelingLevel = true,
                    Sequence = sequence,
                    Description = "لغو سفارش", // "سفارش کنسل شده است",
                    Enabled = true,
                    Type = 0,
                    Description2 = "لغو شده است",
                });      _context.SaveChanges();
            }


            //سفارش برگشت شده است
            sequence = 1;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    ReturningLevel = true,
                    Description = "برگشت سفارش",
                    Enabled = true,
                    Type = 0,
                    Description2 = "برگشت شده است",
                    MessageText = "برگشت",
                });      _context.SaveChanges();
            }

            // ثبت سفارش در حال انجام است
            sequence = 100;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    FirstLevel = true,
                    OrderCanChange = true,
                    Description = "تأیید اطلاعات سفارش",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در انتظار تأیید نهایی سفارش",
                    MessageText = "تأیید نهایی کالاها",
                });      _context.SaveChanges();
            }

            // ثبت سفارش انجام شده است اما سفارش به شرکت ارسال نشده است
            sequence = 200;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    OrderCanChange = true,
                    Description = "تأیید نهایی کالاها",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در انتظار ارسال به شرکت",
                    MessageText = "ارسال سفارش به شرکت",
                });      _context.SaveChanges();
            }

            // سفارش به شرکت ارسال شده است
            sequence = 400;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "ارسال سفارش به شرکت",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در انتظار تأیید توسط واحد فروش",
                    MessageText = "تأیید سفارش و ارجاع به واحد فروش",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد فروش
            sequence = 700;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید سفارش توسط واحد فروش",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در انتظار تأیید توسط مهندسی فروش",
                    MessageText = "تأیید سفارش و ارجاع به مهندسی فروش",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 800;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید سفارش توسط مهندس فروش",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در انتظار تأیید توسط واحد مالی",
                    MessageText = "تأیید سفارش و ارجاع به واحد مالی",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 900;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید سفارش توسط واحد مالی",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در انتظار تولید",
                    MessageText = "تأیید سفارش و ارجاع به برنامه ریزی",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 1000;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید سفارش توسط برنامه ریزی و دفتر فنی",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در حال تولید",
                    MessageText = "تأیید سفارش و ارجاع جهت تولید",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 700;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تأیید سفارش توسط واحد فروش",
                    Enabled = true,
                    Type = 0,
                    Description2 = "",
                    MessageText = "",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 1200;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تکمیل تولید",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در حال ارسال سفارش به انبار",
                    MessageText = "ارسال سفارش به انبار",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 1300;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "ورود سفارش به انبار",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در حال ارسال سفارش از انبار",
                    MessageText = "اعلام خروج سفارش از انبار",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 1400;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "ارسال سفارش از انبار",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در حال انجام نصب",
                    MessageText = "تکمیل نصب",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 1500;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    Description = "تکمیل نصب",
                    Enabled = true,
                    Type = 0,
                    Description2 = "در حال تحویل سفارش به مشتری",
                    MessageText = "تحویل قطعی",
                });      _context.SaveChanges();
            }

            // تأیید شده توسط واحد
            sequence = 1600;
            if (!lstOL.Any(d => d.Sequence == sequence))
            {
                _context.Order_Level.Add(new Models.Order_Level
                {
                    Company_Id = Stack.Company_Id,
                    Sequence = sequence,
                    LastLevel = true,
                    Description = "تحویل قطعی",
                    Enabled = true,
                    Type = 0,
                    Description2 = "تحویل قطعی شده است",
                    //MessageText = "",
                });      _context.SaveChanges();
            }


        }

        // تعریف کاربران مهم
        public void SetUsers()
        {
            CryptographyProcessor cryptographyProcessor = new CryptographyProcessor();
            List<Models.User> lstUsers = _context.User.Where(d => d.Company_Id == Stack.Company_Id).ToList();
            //Program.dbOperations.GetAllUsersAsync(Stack.Company_Id, 0);
            string user_name;
            long user_creator_id = 0;
            if (_context.User.Any(d => d.Name.Equals("real_admin")))
                user_creator_id = _context.User.First(d => d.Name.Equals("real_admin")).Id;

            // کاربر ارشد (ادمین) با سطح 1
            user_name = "admin";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("9999", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "ادمین",
                    Active = true,
                    //User_Level = Stack.UserLevel_Supervisor1,
                    UserLevel_Description = "ادمین",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // کاربر اتوماتیک - کاربر ارشد با سطح 1
            user_name = "Senior";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کاربر ارشد 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Supervisor1,
                    UserLevel_Description = "کاربر ارشد",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }
            // کاربر اتوماتیک - کاربر ارشد با سطح 1
            user_name = "Senior_Auto";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کاربر اتوماتیک",
                    Active = true,
                    //User_Level = Stack.UserLevel_Supervisor1,
                    UserLevel_Description = "کاربر اتوماتیک",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // سرپرست فروش 1
            user_name = "slr1";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "سرپرست فروش 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "سرپرست فروش",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // کارشناس فروش 2
            user_name = "slr2";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کارشناس فروش 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "کارشناس فروش",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // سرپرست برنامه ریزی تولید
            user_name = "Pln1";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "سرپرست برنامه ریزی تولید 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "سرپرست برنامه ریزی تولید",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // کارشناس برنامه ریزی تولید
            user_name = "Pln2";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کارشناس برنامه ریزی تولید 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "کارشناس برنامه ریزی تولید",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // سرپرست واحد مالی
            user_name = "Fin1";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "سرپرست واحد مالی 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "سرپرست واحد مالی",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // کارمند واحد مالی
            user_name = "Fin2";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "کارمند واحد مالی 1",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "کارمند واحد مالی",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

            // عاملیت 1
            user_name = "Agent1";
            if (!lstUsers.Any(d => d.Name.Equals(user_name)))
            {
                _context.User.Add(new Models.User
                {
                    Company_Id = Stack.Company_Id,
                    Password = cryptographyProcessor.GenerateHash("1111", Stack.Standard_Salt),
                    Name = user_name,
                    Real_Name = "عاملیت",
                    Active = true,
                    //User_Level = Stack.UserLevel_Agent,
                    UserLevel_Description = "عاملیت 1",
                    DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now),
                    DateTime_sh = Stack_Methods.DateTimeNow_Shamsi(),
                    End_DateTime_mi = Stack_Methods.DateTime_Miladi(DateTime.Now.AddYears(10)),
                    End_DateTime_sh = Stack_Methods.Miladi_to_Shamsi_YYYYMMDD(DateTime.Now.AddYears(10))
                        + Stack_Methods.NowTime_HHMMSSFFF().Substring(0, 5),
                    User_Id_Creator = user_creator_id,
                });      _context.SaveChanges();
            }

        }

        // تعریف انبار های موجود
        public void SetWareHouses()
        {
            if (!_context.Warehouse.Any(d => d.Company_Id == Stack.Company_Id))
            {
                _context.Warehouse.Add(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار محصول",
                    //Address = "مجاور شاپور جدید",
                    Active = true,
                });      _context.SaveChanges();

                _context.Warehouse.Add(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار ملزومات",
                    Active = true,
                });      _context.SaveChanges();

                _context.Warehouse.Add(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار مواد اولیه",
                    Active = true,
                });      _context.SaveChanges();

                _context.Warehouse.Add(new Models.Warehouse
                {
                    Company_Id = Stack.Company_Id,
                    Name = "انبار نیم ساخت",
                    Active = true,
                });      _context.SaveChanges();

            }
        }


    }
}
