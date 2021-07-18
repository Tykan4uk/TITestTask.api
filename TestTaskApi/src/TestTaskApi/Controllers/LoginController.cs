using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestTaskApi.Configurations;
using TestTaskApi.Models;
using TestTaskApi.Models.Requests;
using TestTaskApi.Services.Abstractions;

namespace TestTaskApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IOptions<AuthConfig> _options;

        public LoginController(
            IAccountService accountService,
            IOptions<AuthConfig> options)
        {
            _accountService = accountService;
            _options = options;
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest addRoleRequest)
        {
            await _accountService.AddRole(addRoleRequest.Role);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest registrationRequest)
        {
            await _accountService.Registration(registrationRequest.Email, registrationRequest.Password, registrationRequest.Role);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authentificationResponse = await _accountService.Authentification(loginRequest.Email, loginRequest.Password);

            if (authentificationResponse.Account != null)
            {
                var token = GenerateJWT(authentificationResponse.Account);

                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }

        private string GenerateJWT(AccountModel accountModel)
        {
            var authParams = _options.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, accountModel.Email),
                new Claim(JwtRegisteredClaimNames.Sub, accountModel.Id)
            };

            claims.Add(new Claim("role", accountModel.Role.RoleName));

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
