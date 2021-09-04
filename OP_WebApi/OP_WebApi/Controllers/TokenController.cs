using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace OP_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                //var tokenString = GenerateJSONWebToken(user);
                //response = Ok(new { token = tokenString });
            }

            return response;
        }




        private LoginModel AuthenticateUser(LoginModel login)
        {
            LoginModel user = null;

            //Validate the User Credentials   
            
            //Demo Purpose, I have Passed HardCoded User Information    
            if (login.UserName == "Jignesh")
            {
                user = new LoginModel { UserName = "Jignesh Trivedi" };//, EmailAddress = "test.btest@gmail.com" };
            }
            return user;
        }

        public class LoginModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }







        //
    }
}