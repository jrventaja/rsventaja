using RSVentaja.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Repository
{
    public interface IUserRepository
    {
        Task<UserTokenData> GetOrAddTokenByCredentials(string user, string password, string token);
        Task<bool> ValidateToken(string token);
    }
}
