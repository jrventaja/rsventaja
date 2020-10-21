using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Repository
{
    public interface IPolicyRepository
    {
        Task<bool> AddPolicy(Policy policy, File file);
        Task<IEnumerable<Policy>> GetPolicies(string query, DateTime startDate, DateTime endDate);
        Task<File> GetFile(int policyId);
        Task<bool> UpdateRenewalStarted(int policyId, bool status);
    }
}
