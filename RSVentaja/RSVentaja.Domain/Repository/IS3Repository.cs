using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Repository
{
    public interface IS3Repository
    {
        public Task StoreFile(string id, File file);
    }
}
