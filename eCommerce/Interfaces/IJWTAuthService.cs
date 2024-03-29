﻿using System.Security.Claims;

namespace eCommerce.Interfaces
{
    public interface IJWTAuthService
    {
        // string SecretKey { get; set; }

        bool IsTokenValid(string token);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
