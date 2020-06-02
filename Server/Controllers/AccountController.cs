using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Server.Models;
using TodoApi.Shared.Models;
using AutoMapper;

namespace WebApiJwt.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<WebApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<WebApiUser> userManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status400BadRequest)]
        [Route("Login", Name="Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);

            if (appUser != null)
            {
                var result = await _userManager.CheckPasswordAsync(appUser, model.Password);
                if (result)
                {
                    var token = GenerateJwtToken(appUser);
                    return Ok(new TokenViewModel(token, "Successful login", DateTime.Now));
                }
            }
            
            return BadRequest(new MessageViewModel("Login failed", DateTime.Now));
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
        [Route("Logout", Name="Logout")]
        public async Task<IActionResult> Logout()
        {
            // Well, What do you want to do here?
            // Wait for token to get expired OR
            // Maintain token cache and invalidate the tokens after logout method is called

            await Task.FromResult(0);

            return Ok(new MessageViewModel("Successful logout.", DateTime.Now));
        }
        
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status400BadRequest)]
        [Route("Register", Name="Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new WebApiUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(user);
                    
                    return CreatedAtRoute("Display", null, new TokenViewModel(token, "Successful registration", DateTime.Now));
                }
            }
            
            return BadRequest(new MessageViewModel("Registration failed.", DateTime.Now));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(DisplayViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status400BadRequest)]
        [Route("Edit", Name="Edit")]
        public async Task<IActionResult> Edit([FromBody] DisplayViewModel model)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentUser = await _userManager.FindByIdAsync(CurrentUserId);

            if(ModelState.IsValid)
            {
                CurrentUser.FirstName = model.FirstName;
                CurrentUser.LastName = model.LastName;
                CurrentUser.Email = model.Email;
                CurrentUser.UserName = model.Email;

                await _userManager.UpdateAsync(CurrentUser);

                return Ok(_mapper.Map<DisplayViewModel>(CurrentUser));
            }
            return BadRequest(new MessageViewModel("Edit failed.", DateTime.Now));
        }

        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status400BadRequest)]
        [Route("ChangePassword", Name="ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordViewModel model)
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentUser = await _userManager.FindByIdAsync(CurrentUserId);

            if(ModelState.IsValid)
            {
                var result = await _userManager.CheckPasswordAsync(CurrentUser, model.CurrentPassword);
                if (result)
                {
                    CurrentUser.PasswordHash = _userManager.PasswordHasher.HashPassword(CurrentUser, model.NewPassword);
                }

                await _userManager.UpdateAsync(CurrentUser);
                
                //or: await _userManager.ChangePasswordAsync(CurrentUser, model.CurrentPassword, model.NewPassword)

                return Ok(new MessageViewModel("Your password changed successfully.", DateTime.Now));
            }
            
            return BadRequest(new MessageViewModel("Password change failed.", DateTime.Now));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(DisplayViewModel), StatusCodes.Status200OK)]
        [Route("Display", Name="Display")]
        public async Task<IActionResult> Display()
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentUser = await _userManager.FindByIdAsync(CurrentUserId);

            return Ok(_mapper.Map<DisplayViewModel>(CurrentUser));
        }

        [Authorize]
        [HttpDelete]
        [ProducesResponseType(typeof(MessageViewModel), StatusCodes.Status200OK)]
        [Route("Delete", Name="Delete")]
        public async Task<IActionResult> Delete()
        {
            var CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var CurrentUser = await _userManager.FindByIdAsync(CurrentUserId);

            await _userManager.DeleteAsync(CurrentUser);

            return Ok(new MessageViewModel("Your account deleted successfully.", DateTime.Now));
        }
        
        private string GenerateJwtToken(WebApiUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            //_logger.LogInformation("user.id: {user.id}", user.Id);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}