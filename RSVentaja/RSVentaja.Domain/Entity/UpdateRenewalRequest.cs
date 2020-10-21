using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.Entity
{
    public class UpdateRenewalRequest
    {
        public int PolicyId { get; set; }
        public bool Status { get; set; }
    }
}
