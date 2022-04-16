using BuyMe.API.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IOptions<AuthConfig> _authConfig;

        public LoginController(IOptions<AuthConfig> authConfig)
        {
            _authConfig = authConfig;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return Ok(_authConfig.Value.AfterHowManySecondsRetrySendingOTP);
        }
    }
}
