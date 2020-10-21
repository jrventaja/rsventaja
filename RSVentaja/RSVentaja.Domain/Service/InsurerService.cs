using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Service
{
    public class InsurerService : IInsurerService
    {
        private readonly IInsurerRepository _insurerRepository;

        public InsurerService(IInsurerRepository insurerRepository)
        {
            _insurerRepository = insurerRepository;
        }
        public async Task<IEnumerable<Insurer>> GetInsurers()
        {
            return await _insurerRepository.GetInsurers();
        }

        public async Task<bool> AddInsurer(string insurer)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            insurer = textInfo.ToTitleCase(insurer);
            return await _insurerRepository.AddInsurer(insurer);
        }
    }
}
