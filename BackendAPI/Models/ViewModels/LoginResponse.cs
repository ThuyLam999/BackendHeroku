using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Models.ViewModels
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public LoginResponse(string jwtToken, string refreshtoken)
        {
            Token = jwtToken;
            RefreshToken = refreshtoken;
        }
    }
}
