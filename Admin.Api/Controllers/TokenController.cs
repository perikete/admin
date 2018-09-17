using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Admin.Api.Core;
using Admin.Api.Data.Entities;
using Admin.Api.Data.Repositories;
using Admin.Api.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Admin.Api.Controllers {
    public class TokenController : Controller {
        private readonly UserManager<User> _userManager;
        private readonly UserRepository _userRepository;
        private readonly JwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public TokenController (UserManager<User> userManager, UserRepository userRepository, JwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions) {
            _userManager = userManager;
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost ("Generate")]
        public async Task<IActionResult> Generate ([FromBody] RegistrationViewModel model) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            var user = new User { LastName = model.LastName, Email = model.Email, FirstName = model.FirstName };

            var result = await _userManager.CreateAsync (user, model.Password);

            if (!result.Succeeded) return BadRequest ("Error creating user");

            _userRepository.AddUser (user);

            return Ok ("Account created");
        }

        [HttpPost ("Authorize")]
        public async Task<IActionResult> Post ([FromBody] CredentialsViewModel credentials) {
            if (!ModelState.IsValid) {
                return BadRequest (ModelState);
            }

            var identity = await GetClaimsIdentity (credentials.Username, credentials.Password);
            if (identity == null) {
                return BadRequest ("Invalid username or password.");
            }

            var jwt = await _jwtFactory.GenerateJwt (identity, _jwtFactory, credentials.Username, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult (jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity (string userName, string password) {
            if (string.IsNullOrEmpty (userName) || string.IsNullOrEmpty (password))
                return await Task.FromResult<ClaimsIdentity> (null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync (userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity> (null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync (userToVerify, password)) {
                return await Task.FromResult (_jwtFactory.GenerateClaimsIdentity (userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity> (null);
        }
    }
}