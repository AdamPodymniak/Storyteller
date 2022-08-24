using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Storyteller.API.Models;
using Storyteller.Repository.Entities;
using Storyteller.Repository.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Storyteller.API.Services
{
    public interface IAuthService
    {
        string Register(UserRegistrationModel request);
        string Login(UserLoginModel request);
        string GenerateInvitation();
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

        public string Login(UserLoginModel request)
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
            if (user.Username == null) return "UsrErr";
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) return "PasErr";

            string token = CreateToken(user);

            return token;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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
            user.Role = "Admin";
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
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        public string GenerateInvitation()
        {
            Invite i = new Invite();
            i.Used = false;
            i.Invitation = Guid.NewGuid();
            _inviteRepository.Add(i);

            return i.Invitation.ToString();
        }
    }
}
