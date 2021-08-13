using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestApiCRUDDemo.DTO;
using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public readonly IConfiguration _configuration;
        public UserController(UserManager<User> userManager,SignInManager<User> signInManager,IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(user,model.Password);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName); 
            if(user==null)
            {
                return BadRequest(new { message = "username is incorrect" });
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if(result.Succeeded)
            {
                return Ok(new {
                    token=GenerateJwtToken(user),
                    });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key =Encoding.ASCII.GetBytes(_configuration.GetSection("Appsettings:Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject=new ClaimsIdentity(new Claim[] { 
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)            
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
         }
    }
}
