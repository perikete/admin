using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Admin.Api.Core.Extensions;
using Admin.Api.Data.Entities;
using Admin.Api.Data.Repositories;
using Admin.Api.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Admin.Api.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController (IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost ("Create")]
        public async Task<IActionResult> Create ([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest (ModelState);
            }

            var user = new User { UserName = model.Email, LastName = model.LastName, Email = model.Email, FirstName = model.FirstName };

            var result = await _userManager.CreateAsync (user, model.Password);

            if (!result.Succeeded) return BadRequest ($"Error creating user: ${ string.Join(",", result.Errors.Select(o => o.Description)) }");

            return Ok ("Account created");
        }

        [HttpPost ("Authenticate")]
        public async Task<IActionResult> Authenticate ([FromBody] CredentialsViewModel credentials)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync (credentials.Username);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync (user, credentials.Password, false);
                    if (result.Succeeded)
                    {

                        var claims = new []
                        {
                            new Claim (JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ()),
                        };

                        var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_configuration.GetKey ()));
                        var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken (_configuration.GetIssuer (),
                            _configuration.GetIssuer (),
                            claims,
                            expires : DateTime.Now.AddMinutes (30),
                            signingCredentials : creds);

                        return Ok (new { token = new JwtSecurityTokenHandler ().WriteToken (token) });
                    }
                }
            }

            return BadRequest ("Could not create token");
        }
    }
}