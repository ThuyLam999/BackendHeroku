using BackendAPI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public ERole Role { get; set; }
        public string VerificationShortToken { get; set; }
        public string ResetToken { get; set; }
        public DateTime? DateResetTokenExpired { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
