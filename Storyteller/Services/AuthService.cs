using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Storyteller.API.Models;
using Storyteller.Repository.Entities;
using Storyteller.Repository.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Storyteller.API.Services
{
    public interface IAuthService
    {
        User GetUserFromName(string name);
        string Register(UserRegistrationModel request);
        ResponseTokenModel Login(UserLoginModel request);
        string GenerateInvitation(string role);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string CreateJWTToken(User user);
        string CreateRefreshToken();
    }
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IInviteRepository _inviteRepository;
        public AuthService(IUserRepository userRepository, IConfiguration configuration, IInviteRepository inviteRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _inviteRepository = inviteRepository;
        }

        public ResponseTokenModel? Login(UserLoginModel request)
        {
            User user = new User();
            IEnumerable<User> users = _userRepository.GetList();
            foreach(var u in users)
            {
                if(u.Username == request.Username)
                {
                    user = u;
                }
            }
            if (user.Username == null) return null;
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) return null;

            ResponseTokenModel token = new ResponseTokenModel();

            token.JWTToken = CreateJWTToken(user);
            token.RefreshToken = CreateRefreshToken();
            token.Role = user.Role;

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1);
            _userRepository.Update(user);

            return token;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        public string CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Guid.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("AppSettings:issuer").Value,
                audience: _configuration.GetSection("AppSettings:audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public string Register(UserRegistrationModel request)
        {
            Invite myInvitation = CheckInvitation(request.Invitation);
            if (myInvitation == null) return "InvErr";
            IEnumerable<User> registeredUsers = _userRepository.GetList();
            foreach(var u in registeredUsers)
            {
                if (u.Username == request.Username) return "UsrErr";
            }

            User user = new User();
            Guid guid = Guid.NewGuid();

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Username = request.Username;
            user.Rating = 0;
            user.Role = myInvitation.Role;
            user.Guid = guid;

            _userRepository.Add(user);

            myInvitation.Used = true;
            _inviteRepository.Update(myInvitation);

            return "User added";
        }

        private Invite CheckInvitation(string invitation)
        {
            IEnumerable<Invite> invites = _inviteRepository.GetNotUsed();
            if (!Guid.TryParse(invitation, out var newGuid)) return null;
            Guid inviteKey = Guid.Parse(invitation);
            foreach (var i in invites)
            {
                if (i.Invitation == inviteKey) return i;
            }
            return null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        
        public string GenerateInvitation(string role)
        {
            Invite i = new Invite();
            i.Used = false;
            i.Invitation = Guid.NewGuid();
            i.Role = role;
            _inviteRepository.Add(i);

            return i.Invitation.ToString();
        }

        public User GetUserFromName(string name)
        {
            var user = _userRepository.GetByName(name);
            return user;
        }
    }
}
