using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Service;

namespace RSVentaja.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("token")]
        [HttpPost]
        public async Task<ActionResult> GetToken([FromBody]TokenRequest tokenRequest)
        {
            var userTokenData = await _authenticationService.GetToken(tokenRequest.Username, tokenRequest.Password);
            if (userTokenData != null)
                return Ok(userTokenData);
            return Unauthorized();
        }

        [Route("token/verify")]
        [HttpPost]
        public async Task<ActionResult> VerifyToken([FromBody]VerifyTokenRequest tokenRequest)
        {
            return Ok(new VerifyTokenResponse { Valid = await _authenticationService.ValidateToken(tokenRequest.Token) });
        }
    }
}