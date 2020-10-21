using RSVentaja.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Service
{
    public interface IAuthenticationService
    {
        Task<bool> ValidateToken(string token);
        Task<UserTokenData> GetToken(string username, string password);
    }
}
