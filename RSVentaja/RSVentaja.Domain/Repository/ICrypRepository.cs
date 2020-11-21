using System;
using System.Collections.Generic;
using System.Text;

namespace RSVentaja.Domain.Repository
{
    public interface ICrypRepository
    {
        public string SignData(string data);
    }
}
