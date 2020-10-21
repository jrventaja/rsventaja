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
    public class InsurerController : ControllerBase
    {
        private readonly IInsurerService _insurerService;
        private readonly IAuthenticationService _authenticationService;

        public InsurerController(IInsurerService insurerService, IAuthenticationService authenticationService)
        {
            _insurerService = insurerService;
            _authenticationService = authenticationService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insurer>>> GetInsurers()
        {
            if (Request.Headers.TryGetValue("Token", out var value) && await _authenticationService.ValidateToken(value.First()))
                return Ok(await _insurerService.GetInsurers());
            return BadRequest();
        }
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Insurer>>> AddInsurer([FromBody]AddInsurerRequest addInsurerRequest)
        {
            if (Request.Headers.TryGetValue("Token", out var value) && await _authenticationService.ValidateToken(value.First()))
                return Ok(await _insurerService.AddInsurer(addInsurerRequest.Insurer));
            return BadRequest();
        }
    }
}