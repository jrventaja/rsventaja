using RSVentaja.Domain.Entity;
using RSVentaja.Domain.Repository;
using RSVentaja.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSVentaja.Domain.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserTokenData> GetToken(string username, string password)
        {
            var token = Guid.NewGuid().ToString("D");
            return await _userRepository.GetOrAddTokenByCredentials(username, password, token);
        }

        public async Task<bool> ValidateToken(string token)
        {
            return await _userRepository.ValidateToken(token);
        }

    }
}
