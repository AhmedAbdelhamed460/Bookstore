using Bookstore.Models;
using Bookstore.DOT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationDTO registrationDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    firstName = registrationDTO.FirstName,
                    LastName = registrationDTO.LastName,
                    UserName = registrationDTO.UserName,
                    Email = registrationDTO.Email,
                    PhoneNumber = registrationDTO.PhoneNumber,
                    Address = registrationDTO.Address             
                };
                IdentityResult result = await userManager.CreateAsync(user, registrationDTO.Password);
                registrationDTO.Id = user.Id;
                if (result.Succeeded) return Ok(registrationDTO);
                else return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("/api/userlogin")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
                AppUser? user = await userManager.FindByEmailAsync(loginDTO.Email);
                if (user != null)
                {
                    bool valid = await userManager.CheckPasswordAsync(user, loginDTO.Password);
                    if (valid)
                    {
                        // await signInManager.SignInAsync(user, loginVM.RememberMe);
                        //List<Claim> claims = new List<Claim>()
                        //{
                        //    new Claim("Address",user.Address),
                        //    new Claim("Age",user.Age.ToString())
                        //};

                        //Generate Token
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_123456"));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));

                    }
                    else return Unauthorized();
                }
             return Unauthorized();

        }
    }
}

