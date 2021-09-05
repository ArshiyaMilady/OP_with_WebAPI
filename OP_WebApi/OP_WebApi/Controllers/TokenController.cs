using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OP_WebApi.Models;

namespace OP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly TableContext _context;

        public TokenController(IConfiguration config, TableContext context)
        {
            _config = config;
            _context = context;
        }

        //// api/Token
        //[AllowAnonymous]
        //[HttpPost]
        //public string CreateToken([FromBody]LoginModel login)
        //{
        //    string response = null;
        //    User user = AuthenticateUser(login);

        //    if (user != null)
        //    {
        //        response = BuildToken(); 
        //    }

        //    return response;
        //}


        // api/Token
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            var tokenString = BuildToken();

            if (user != null)
            {
                response = Ok(new { token = tokenString });
            }

            return response;
        }


        private User AuthenticateUser(LoginModel login)
        {
            User user = null;
            string hashed_password = new CryptographyProcessor().GenerateHash(login.Password, Stack.ConstantKey);
            //Validate the User Credentials   
            //if (_context.User.Where(d=>d.Active).Where(j=>j.Name.ToLower().Equals(login.UserName.ToLower()))
            //    .Any(n=>n.Password.Equals(hashed_password)))

            if (login.LoginType == 1)
            {
                user = _context.User.Where(d => d.Active).Where(j => j.Name.ToLower().Equals(login.UserName_Mobile.ToLower()))
                    .FirstOrDefault(n => n.Password.Equals(hashed_password));
            }
            else if (login.LoginType == 2)
            {
                string mobile = login.UserName_Mobile;
                if (mobile.Length < 12)
                    mobile = "0098" + mobile.Substring(mobile.Length - 10);
                user = _context.User.Where(d => d.Active).Where(j => j.Phone.Equals(login.UserName_Mobile))
                    .FirstOrDefault(n => n.Password.Equals(hashed_password));
            }

            // رمز ورود را بر نگرداند
            if (user != null) user.Password = null;

            return user;
        }

        private string BuildToken()
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //{"LoginType":"1","UserName_Mobile":"admin","Password":"9999"}
        public class LoginModel
        {
            // LoginType = 1 : ورود با شناسه کاربری
            // LoginType = 2 : ورود با تلفن همراه
            public int LoginType { get; set; }
            public string UserName_Mobile { get; set; }
            public string Password { get; set; }
        }







        //
    }
}