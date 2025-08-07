using Microsoft.AspNetCore.Identity;
using service.authapi.Data;
using service.authapi.Models;
using service.authapi.Models.DTO;
using System.Linq;

namespace service.authapi.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<List<MPermissions>> GetPermissonByRoleID(int RoleID);
    }

    public class AuthService : IAuthService
    {
        private readonly AuthDbContext _db;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AuthDbContext db, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<List<MPermissions>> GetPermissonByRoleID(int RoleID)
        {
            var rolePermissons = _db.RolePermissons.Where(u => u.RoleId == RoleID).Select(z => z.PermissionId).ToList();
            var permission = _db.MPermissions.Where(s => rolePermissons.Contains(s.Id)).ToList();
            return permission;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email.ToLower() == loginRequestDto.Email.ToLower());
            if (user == null)
            {
                return new LoginResponseDto() { Message = "User Not Found" };
            }

            bool isValid = user.Password.ToLower() == loginRequestDto.Password.ToLower();
            if (isValid == false)
            {
                return new LoginResponseDto() { Message = "Invalid Username and Password" };
            }

            //if user was found, Generate JWT Token
            var token = _jwtTokenGenerator.GenerateToken(user);//, roles);

            UserProfile userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                FullName = user.FullName,
                RoleID = user.RolesId
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }
    }
}
