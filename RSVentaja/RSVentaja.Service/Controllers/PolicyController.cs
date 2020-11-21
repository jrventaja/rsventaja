using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Service;
using RSVentaja.Domain.ValueObjects;

namespace RSVentaja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly IAuthenticationService _authenticationService;

        public PolicyController(IPolicyService policyService, IAuthenticationService authenticationService)
        {
            _policyService = policyService;
            _authenticationService = authenticationService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicies(string searchTerm, bool currentOnly)
        {
            if (Request.Headers.TryGetValue("Token", out var value) && await _authenticationService.ValidateToken(value.First()))                
                return Ok(await _policyService.GetPolicies(searchTerm, currentOnly));            
            return Unauthorized();
        }
        [Route("duePolicies")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPoliciesLast30Days()
        {
            if (Request.Headers.TryGetValue("Token", out var value) && await _authenticationService.ValidateToken(value.First()))
                return Ok(await _policyService.GetPoliciesLast30Days());
            return Unauthorized();
        }
        [Route("new")]
        [HttpPost]
        public async Task<ActionResult> AddPolicy([FromBody]AddPolicyRequest addPolicyRequest)
        {
            if (Request.Headers.TryGetValue("Token", out var value) && await _authenticationService.ValidateToken(value.First()))
            {
                var policy = _policyService.GetPolicyFromRequest(addPolicyRequest);
                if (!policy.Item1.Valid || !policy.Item2.Valid)
                    return BadRequest();
                await _policyService.AddPolicy(policy.Item1, policy.Item2);
                return Ok();
            }
            return Unauthorized();
        }
        [Route("updateRenewal")]
        [HttpPost]
        public async Task<ActionResult<GetFileResponse>> UpdateRenewalStarted([FromBody]UpdateRenewalRequest request)
        {
            if (Request.Headers.TryGetValue("Token", out var value) && await _authenticationService.ValidateToken(value.First()))
            {
                if (await _policyService.UpdateRenewalStarted(request.PolicyId, request.Status))                
                    return Ok();
                return NotFound();
            }
            return Unauthorized();
        }
    }
}
