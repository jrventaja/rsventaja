using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.Entity
{
    public class AddPolicyRequest
    {
        public string Name { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Insurer { get; set; }
        public int InsurerId { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
    }
}
