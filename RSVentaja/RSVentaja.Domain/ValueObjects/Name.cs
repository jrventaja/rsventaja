using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.ValueObjects
{
    public class Name : Verifiable
    {
        public Name(string fullName)
        {
            Assert(() => !string.IsNullOrWhiteSpace(fullName));
            var firstName = fullName.Trim().Split(" ")[0];
            FirstName = firstName;
            LastName = fullName.Replace(firstName, "").Trim();
        }

        public override string ToString()
        {
            return FirstName + LastName;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }
    }
}
