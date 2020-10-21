using RSVentaja.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Repository
{
    public interface IInsurerRepository
    {
        Task<IEnumerable<Insurer>> GetInsurers();

        Task<bool> AddInsurer(string insurer);
    }
}
