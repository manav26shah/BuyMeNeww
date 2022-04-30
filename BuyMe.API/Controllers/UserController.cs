using BuyMe.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BuyMe.API.Filters;
using Microsoft.Extensions.Logging;
using BuyMe.BL.Interface;
using BuyMe.BL;
using BuyMe.API.DTO.Response;
using Microsoft.AspNetCore.Http;
using BuyMe.API.DTO.Request;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using BuyMe.API.Config;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BuyMe.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [LoggingFilter]
    public class UserController:ControllerBase
    {
       
        private ILogger<UserController> _logger;
        private IUserManagementService _userManager;
        private IOptions<JwtConfig> _jwtConfig;

        public UserController(IUserManagementService userManager, ILogger<UserController> logger, IOptions<JwtConfig> config)
        {
           
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = config;
        }
        
        [HttpGet("{userId}")]
        public IActionResult GetUserDetails([FromRoute]int userId)
        {

            _logger.LogInformation("Request originated");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserRequest data)
        {
            try
            {
                var userBL = new RegisterUserBL
                {
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    PhoneNumber = data.PhoneNumber,
                    EmailId = data.EmailId,
                    Password = data.Password,
                    Address = data.Address,
                    DateOfBirth = data.DateOfBirth
                };
                var result = await _userManager.RegisterUser(userBL);
                _logger.LogDebug("User Db contacted successfully");
                if (result.Item1)
                {
                    _logger.LogDebug($"User {data.EmailId} created contacted successfully");
                    var res = new Response();
                    res.Message.Add("User Created Succesfully");
                    return Ok(res);
                }
                else
                {
                    var res = new Response();
                    res.Message = result.Item2;
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("authtoken")]
        public async Task<IActionResult> Login([FromBody] LoginRequest data)
        {
            var result = await _userManager.Login(data.EmailId, data.Password);
            if (result)
            {
                var res = GenerateJWt(data.EmailId);
                return Ok(new Response<LoginResponse> { Data = res });
            }
            else
            {
                var res = new Response();
                res.Message.Add("Incorrect EmailId or Password");
                return BadRequest(res);
            }
        }
        private LoginResponse GenerateJWt(string email)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim("Role","Admin")
            };

            // Form the security key
            var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Value.Secret));

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Value.ValidIssuer,
                audience: _jwtConfig.Value.ValidAudience,
                expires: DateTime.Now.AddMinutes(55),
                claims: authClaims,
                signingCredentials: new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256)
                ) ;
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            var expiry = token.ValidTo;
            return new LoginResponse
            {
                Token = jwtToken,
                Expiry = expiry
            };
        }

    }
}
