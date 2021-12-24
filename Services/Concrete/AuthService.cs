using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectTracker_API.DataAccess.DataAccessLayers.Abstract;
using ProjectTracker_API.DataAccess.RepositoryBases.Abstract;
using ProjectTracker_API.DataAccess.RepositoryBases.Concrete;
using ProjectTracker_API.Helpers;
using ProjectTracker_API.Models.Concretes;
using ProjectTracker_API.Models.Constants;
using ProjectTracker_API.Models.DTOs.UserDTOs;
using ProjectTracker_API.Models.Entities;
using ProjectTracker_API.Services.Abstract;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracker_API.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IUserDAL _entityDAL;
        public AuthService(IUserDAL entityDAL, IMapper mapper)
        {
            _entityDAL = entityDAL;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginDTO user)
        {
            if ((user.UserName ?? user.Password) == null) return Responses<string>.FailedResponse(Messages.MailAndPassCantBeNull);
            user.Password = CryptoHelper.CryptString(user.Password);
            var selectedUser = await _entityDAL.GetAsync(u => u.UserName == user.UserName && u.Password == user.Password);
            if (selectedUser == null) return Responses<string>.FailedResponse(Messages.WrongMailOrPassword);
            var tokenString = CreateToken(selectedUser);
            return tokenString is null ? Responses<string>.FailedResponse() : Responses<string>.SuccessResponse(tokenString);
        }

        public async Task<ServiceResponse<string>> RegisterAsync(RegisterDTO user)
        {

            var password = user.Password;
            user.Password = CryptoHelper.CryptString(password);
            var checkUser = await _entityDAL.CheckUser(u => u.UserName == user.UserName);
            if (checkUser) return Responses<string>.FailedResponse(Messages.MailMustBeUniqe);
            var registerUser = PropertyHelper<User>.FillPropForSystem(_mapper.Map<User>(user));
            var CheckRegister = await _entityDAL.AddAsync(registerUser);
            if (!CheckRegister) return Responses<string>.FailedResponse(Messages.SystemFail);
            user.Password = password;
            var userResponse = await LoginAsync(_mapper.Map<LoginDTO>(user));
            return userResponse;
        }

        private static string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Environment.GetEnvironmentVariable("Token");
            if(tokenKey is null) return null;
            var key = Encoding.ASCII.GetBytes(tokenKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}