using BackendAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackendAPI.Service
{
    public interface ITokenService
    {
        LoginResponse Authenticate(LoginRequest model, string ipAddress);
        LoginResponse RefreshToken(string token, string ipAddress);
        bool RevokeToken(string token, string ipAddress);
        Task SendAsync(MailRequest request);
    }
}
