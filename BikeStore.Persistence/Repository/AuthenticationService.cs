using Azure.Core;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Persistence.Repository
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private ApplicationUser _applicationUser;
        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _applicationUser = new ApplicationUser();
        }

        public async  Task<dynamic> GenerateJWtTokenAsync()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:key"]));

            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_applicationUser);

            var roleclaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            List<Claim> claims = new List<Claim>()
            {
                new Claim (JwtRegisteredClaimNames.Email,_applicationUser.Email),
                new Claim(ClaimTypes.Name,$"{_applicationUser.FirstName} {_applicationUser.LastName}") 

            }.Union(roleclaims).ToList();

            var token = new JwtSecurityToken
            (
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                signingCredentials: signingCredential,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]))
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();

            _applicationUser = await _userManager.FindByEmailAsync(request.UserName);
          
            if (_applicationUser == null) 
            {
                response.Message = "Invalid email address";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(_applicationUser, request.Password, false,false);

            var isValidCredential = await _userManager.CheckPasswordAsync(_applicationUser, request.Password);
            if (result.Succeeded)
            {

                var Token = await GenerateJWtTokenAsync();
                response.Token = Token;
                response.UserName = _applicationUser.UserName;

                return response;
            }

            else {

                if (result.IsNotAllowed) 
                {

                    response.Message = "Please verify email address";
                    return response;
                }

                if (isValidCredential is false) {
                    response.Message = "Invalid Password";
                    return response;
                }

                if (result.IsLockedOut) { 
                    response.Message = "Your account is locked contact system admin";
                    return response;

                }
             
            }
            response.Message = "Login failed";
            return response;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<RegisterResponse> RegisterAsync(UserRegisterRequest request)
        {
            RegisterResponse response = new RegisterResponse();
            _applicationUser.UserName = request.Email;
            _applicationUser.Email = request.Email;
            _applicationUser.FirstName = request.FirstName;
            _applicationUser.LastName = request.LastName;
            _applicationUser.Avatar = request.Avatar;

            var result = await _userManager.CreateAsync(_applicationUser, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_applicationUser, "SUPERADMIN");
                response.IsSuccess = true;
                return response;
            }
            else
            {
                response.IsSuccess = false;
                response.Errors = result.Errors.Select(x => x.Description).ToList();
                return response ;
            }

        }
    }
}
