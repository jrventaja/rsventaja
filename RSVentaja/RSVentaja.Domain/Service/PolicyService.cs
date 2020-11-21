using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Repository;
using RSVentaja.Domain.ValueObjects;

namespace RSVentaja.Domain.Service
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _policyRepository;
        private readonly IS3Repository _s3Repository;
        private readonly ICrypRepository _crypRepository;

        public PolicyService (IPolicyRepository policyRepository, IS3Repository s3Repository, ICrypRepository crypRepository)
        {
            _policyRepository = policyRepository;
            _s3Repository = s3Repository;
            _crypRepository = crypRepository;
        }

        public Tuple<Policy, File> GetPolicyFromRequest(AddPolicyRequest addPolicyRequest)
        {
            var policy = new Tuple<Policy, File>(new Policy(addPolicyRequest), new File(addPolicyRequest.FileName, Convert.FromBase64String(addPolicyRequest.File)));
            return policy;
        }

        public async Task<IEnumerable<Policy>> GetPolicies(string searchTerm, bool currentOnly = false)
        {
            if (currentOnly)
                return await _policyRepository.GetPolicies(searchTerm, DateTime.Now, DateTime.MaxValue);
            return await _policyRepository.GetPolicies(searchTerm, DateTime.MinValue, DateTime.MaxValue);
        }

        public async Task<bool> AddPolicy(Policy policy, File file)
        {
            var id = await _policyRepository.AddPolicy(policy);
            if (id > 0)
            {
                await _s3Repository.StoreFile(_crypRepository.SignData(id.ToString()), file);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Policy>> GetPoliciesLast30Days()
        {
            return await _policyRepository.GetPolicies(string.Empty, DateTime.Now, DateTime.Now.AddDays(30));
        }

        public async Task<bool> UpdateRenewalStarted(int policyId, bool status)
        {
            return await _policyRepository.UpdateRenewalStarted(policyId, status);
        }
    }
}
