using BackendAPI.Data;
using BackendAPI.Helpers;
using BackendAPI.Models;
using BackendAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace BackendAPI.Service
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;
        private readonly IMailService _mailService;
        private readonly ApplicationContext _context;
        private readonly int m_refreshTokenDayTimeLive = 7;
        public TokenService(ApplicationContext context, IOptions<AppSettings> appSettings, IMailService mailService)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _mailService = mailService;
        }

        public LoginResponse Authenticate(LoginRequest model, string ipAddress)
        {
            var findUser = _context.Users.Include(a => a.RefreshTokens).Where(a => a.Username == model.Username).FirstOrDefault();

            if (findUser is null)
                return null;

            bool verified = BC.Verify(model.Password, findUser.Password);

            if (!verified)
                return null;

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = GenerateJwtToken(findUser);
            var refreshToken = GenerateRefreshToken(ipAddress);

            //save refresh token
            if (findUser.RefreshTokens == null) findUser.RefreshTokens = new List<RefreshToken>();
            findUser.RefreshTokens.Add(refreshToken);

            //remove old refresh token from user
            RemoveOldRefreshTokens(findUser);

            //here is saving to db
            _context.Update(findUser);
            _context.SaveChanges();

            return new LoginResponse(jwtToken, refreshToken.Token);
        }

        public LoginResponse RefreshToken(string token, string ipAddress)
        {
            var user = _context.Users.Include(a => a.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);

            //remove old refresh token from user
            RemoveOldRefreshTokens(user);

            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = GenerateJwtToken(user);

            return new LoginResponse(jwtToken, newRefreshToken.Token);
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            var user = _context.Users.Include(a => a.RefreshTokens).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.Update(user);
            _context.SaveChanges();

            return true;
        }

        public async Task SendAsync(MailRequest request)
        {
            await _mailService.SendEmailAsync(request);

            var l_sixDigitToken = RandomSixDigitToken();

            var user = new User()
            {
                Id = 0,
                Username = request.Username,
                Email = request.Email,
                Password = BC.HashPassword(request.Password),
                FullName = request.FullName,
                Role = Enums.ERole.User,
                DateResetTokenExpired = null,
                ResetToken = "",
                VerificationShortToken = l_sixDigitToken
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, ((int)user.Role).ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            RefreshToken refreshToken = new RefreshToken();
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                refreshToken = new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    CreatedByIp = ipAddress
                };
            }
            return refreshToken;
        }

        private void RemoveOldRefreshTokens(User user)
        {
            var l_tokens = user.RefreshTokens.Where(_ => !_.IsActive && _.Expires.AddDays(m_refreshTokenDayTimeLive) <= DateTime.UtcNow).Select(_ => _);
            for (int i = 0; i < l_tokens.Count(); i++)
            {
                _context.RefreshTokens.Remove(l_tokens.ElementAt(i));
            }
        }

        public string RandomSixDigitToken()
        {
            Random l_random = new Random();
            int l_sixDigit = l_random.Next(100000, 999999);
            return l_sixDigit.ToString();
        }

    }
}
