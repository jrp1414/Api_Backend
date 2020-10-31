using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using System.Web.Http;
using APIAngular.DBContext;

namespace APIAngular.Controllers
{
    public class AccountController : ApiController
    {
        private StudentDB DB;

        public AccountController()
        {
            DB = new StudentDB();
        }
        public IHttpActionResult Index()
        {
            return Ok();
        }

        [HttpPost]
        [Route("SignIn")]
        public IHttpActionResult SignIn(User user)
        {
            try
            {
                var userData = DB.Users.FirstOrDefault(m => m.EmailId == user.EmailId && m.Password == user.Password);
                if (userData==null)
                {
                    return Unauthorized();
                }
                string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
                var issuer = "https://localhost:44319/";  //normally this will be your site URL    

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //Create a List of Claims, Keep claims name short    
                var permClaims = new List<Claim>();
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim("valid", "1"));
                permClaims.Add(new Claim("userid", userData.UserId.ToString()));
                permClaims.Add(new Claim("name", string.Concat(userData.FirstName," ",userData.LastName)));

                //Create Security Token object by giving required parameters    
                var token = new JwtSecurityToken(issuer, //Issure    
                                issuer,  //Audience    
                                permClaims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { data = jwt_token });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("SignUp")]
        public IHttpActionResult SignUp(User user)
        {
            try
            {
                user.LastUpdated = DateTime.Now;
                DB.Users.Add(user);
                DB.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
    }
}
