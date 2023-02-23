using Bookstore.Models;
using Bookstore.DOT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                {   firstName = registrationDTO.FirstName,
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
    }
}
