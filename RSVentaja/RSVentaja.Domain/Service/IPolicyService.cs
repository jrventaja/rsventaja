using RSVentaja.Domain.Entity;
using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Service
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetPolicies(string searchTerm, bool currentOnly);
        Task<IEnumerable<Policy>> GetPoliciesLast30Days();

        Tuple<Policy, File> GetPolicyFromRequest(AddPolicyRequest addPolicyRequest);

        Task<bool> AddPolicy(Policy policy, File file);
        Task<bool> UpdateRenewalStarted(int policyId, bool status);
    }
}
