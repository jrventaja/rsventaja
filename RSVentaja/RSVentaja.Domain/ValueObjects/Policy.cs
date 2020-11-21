using RSVentaja.Domain.Entity;
using System;

namespace RSVentaja.Domain.ValueObjects
{
    public class Policy : Verifiable
    {
        public int PolicyId { get; private set; }
        public Name CustomerName { get; private set; }
        public string AdditionalInfo { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool RenewalStarted { get; set; }
        public string Insurer { get; private set; }
        public int InsurerId { get; private set; }
        public string FileName { get; set; }

        public Policy( int policyId, Name customerName, DateTime startDate, DateTime endDate, string insurer)
        {
            Assert(() => customerName.Valid);
            Assert(() => !string.IsNullOrEmpty(insurer));
            Assert(() => startDate != null);
            Assert(() => endDate != null);
            Assert(() => endDate.CompareTo(startDate) > 0);
            PolicyId = policyId;
            CustomerName = customerName;
            StartDate = startDate;
            EndDate = endDate;
            Insurer = insurer;
        }

        public Policy (AddPolicyRequest addPolicyRequest)
        {
            CustomerName = new Name(addPolicyRequest.Name);
            Assert(() => CustomerName.Valid);
            Assert(() => addPolicyRequest.StartDate != null);
            Assert(() => addPolicyRequest.EndDate != null);
            Assert(() => addPolicyRequest.EndDate.CompareTo(addPolicyRequest.StartDate) > 0);
            Assert(() => addPolicyRequest.InsurerId > 0);
            AdditionalInfo = addPolicyRequest.AdditionalInfo;
            StartDate = addPolicyRequest.StartDate;
            EndDate = addPolicyRequest.EndDate;
            Insurer = addPolicyRequest.Insurer;
            InsurerId = addPolicyRequest.InsurerId;
        }

        


    }
}
