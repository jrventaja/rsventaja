using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.Entity
{
    public class UserTokenData
    {
        public string Username { get; set; }
        public Name Name { get; set; }
        public string Token { get; set; }
    }
}
