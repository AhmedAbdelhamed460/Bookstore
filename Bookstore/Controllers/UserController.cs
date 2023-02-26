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

        [HttpGet]
        public async Task<ActionResult> getAllUsers()
        {
            List<AppUser> users = await userManager.Users.ToListAsync();
            if (users.Count != 0) return Ok(users);
            else return NotFound();
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
                IdentityResult addUserResult = await userManager.CreateAsync(user, registrationDTO.Password);
                IdentityResult addRoleToUserResult = await userManager.AddToRoleAsync(user, "Customer");
                registrationDTO.Id = user.Id;
                if (addUserResult.Succeeded && addRoleToUserResult.Succeeded) 
                {
                    return Ok(registrationDTO);
                } 
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

        [HttpPost("/api/userToRole")]
        public async Task<ActionResult> addRoleToUser(RoleToUserDTO roleToUserDTO)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in roleToUserDTO.UserIds)
                {
                    AppUser user = await userManager.FindByIdAsync(userId);
                    IdentityResult result = await userManager.AddToRolesAsync(user , roleToUserDTO.RoleNames);
                    if (result.Succeeded == false)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest();
                    }                                     
                }
                return Ok(roleToUserDTO);
            }
            return BadRequest();
        }
        [HttpGet("/api/userProfile")]
       
        public ActionResult profile()
        {
           var str =  User.Identity.IsAuthenticated;
           return Ok();
        }

    }
}

