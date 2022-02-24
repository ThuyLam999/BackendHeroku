using BackendAPI.Data;
using BackendAPI.Errors;
using BackendAPI.Models;
using BackendAPI.Models.ViewModels;
using BackendAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly string m_tokenKeyName = "refreshToken";
        public AccountsController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Send(MailRequest request)
        {
            await _tokenService.SendAsync(request);

            return Ok(new RegisterRequest()
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                FullName = request.FullName
            });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            var response = _tokenService.Authenticate(request, IpAddress());

            if (response == null)
                return Unauthorized();

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh()
        {
            var refreshToken = Request.Cookies[m_tokenKeyName];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest();
            }

            var response = _tokenService.RefreshToken(refreshToken, IpAddress());

            if (response == null)
                return Unauthorized();

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost]
        [Route("revoke")]
        public IActionResult Revoke(RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies[m_tokenKeyName];

            if (string.IsNullOrEmpty(token))
                return BadRequest();

            var response = _tokenService.RevokeToken(token, IpAddress());

            if (!response)
                return NotFound();

            return Ok(new { message = "Token revoked" });
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Strict,
            };
            Response.Cookies.Append(m_tokenKeyName, token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}