using RSVentaja.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Service
{
    public interface IInsurerService
    {
        Task<IEnumerable<Insurer>> GetInsurers();
        Task<bool> AddInsurer(string insurer);
    }
}
