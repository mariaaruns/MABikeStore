using Azure.Core;

using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using BikeStore.Persistence.Data;
using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly BikeStoresContext _dbContext;
        private readonly IConfiguration _config;
        private ApplicationUser _applicationUser;
        public AuthenticationService(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
        BikeStoresContext dbContext,IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            this._dbContext = dbContext;
            _config = config;
            _applicationUser = new ApplicationUser();
        }

        public async Task<AddRoleClaimResponse> AddRoleClaimAsync(AddRoleClaimRequest request)
        {
            var result = new AddRoleClaimResponse();
            result.IsSuccess = false;
            result.ErrorDesc = "Invalid role name";
            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if (role != null)
            {

                var addRoleClaims = await _roleManager.AddClaimAsync(role, new Claim(request.ClaimType, request.ClaimValue));
                if (addRoleClaims.Succeeded)
                {
                    result.IsSuccess = true;
                }
                else if(addRoleClaims.Errors!=null)
                {
                    result.ErrorDesc = string.Join(",",addRoleClaims.Errors.Select(x => x.Description).ToArray());
                }
               
            }
            return result;

        }

        public async Task<ChangeUserRoleResponse> ChangeUserRoleAsync(ChangeUserRoleRequest request)
        {
            var result = new ChangeUserRoleResponse();
            result.IsSuccess = false;
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user != null)
            {
                // Remove from old role
                var removeResult = await _userManager.RemoveFromRoleAsync(user, request.OldUserRole);

                if (removeResult.Succeeded)
                {
                    var addResult = await _userManager.AddToRoleAsync(user, request.NewUserRole);

                    if (addResult.Succeeded)
                    {
                        result.IsSuccess = true;
                    }
                    else
                    {
                        result.ErrorDesc = string.Join(',', addResult.Errors.Select(x => x.Description).ToArray());
                    }
                }

            }
            else {
                result.ErrorDesc = "Invalid User";
            }
            return result;
        }

        public async Task<AddRoleClaimResponse> DeleteRoleClaimAsync(AddRoleClaimRequest request)
        {
            var result = new AddRoleClaimResponse();
            result.IsSuccess = false;
            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if (role != null)
            {
                var RemoveClaims = await _roleManager.RemoveClaimAsync(role, new Claim(request.ClaimType, request.ClaimValue));

                if (RemoveClaims.Succeeded)
                {
                    result.IsSuccess = false;
                }
                else if (RemoveClaims.Errors != null)
                {
                    result.ErrorDesc = string.Join(",", RemoveClaims.Errors.Select(x => x.Description).ToArray());
                }

            }

            return result;
        }

        public async Task<EditRoleClaimResponse> EditRoleClaimAsync(EditRoleClaimRequest request)
        {
            var result = new EditRoleClaimResponse();
            result.IsSuccess = false;
            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if (role != null)
            {
                var RemoveClaims = await _roleManager.RemoveClaimAsync(role, new Claim(request.OldClaimType, request.OldClaimValue));

                if (RemoveClaims.Succeeded)
                {
                    var addRoleClaims = await _roleManager.AddClaimAsync(role, new Claim(request.ClaimType, request.ClaimValue));
                   
                    if (addRoleClaims.Succeeded)
                    {
                        result.IsSuccess = true;
                    }
                    else if (addRoleClaims.Errors != null)
                    {
                        result.ErrorDesc = string.Join(",", addRoleClaims.Errors.Select(x => x.Description).ToArray());
                    }
                }
                else {

                    result.ErrorDesc = string.Join(",", RemoveClaims.Errors.Select(x => x.Description).ToArray());
                }

            }
            return result;
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

        public async Task<IQueryable<GetUserResponse>> GetAllUser(GetUserRequest request)
        {
            var result = _dbContext.Users.Join(_dbContext.UserRoles,
                user => user.Id, userRole => userRole.UserId, (user, userRole) => new { user, userRole })
                .Join(_dbContext.Roles, combined => combined.userRole.RoleId, role => role.Id, (combined, role) => new { combined.user, role })
                 .Where(x =>
                 (string.IsNullOrEmpty(request.FirstName) || x.user.FirstName.Contains( request.FirstName))
                 && (string.IsNullOrEmpty(request.LastName) || x.user.LastName.Contains( request.LastName))
                 && (string.IsNullOrEmpty(request.UserName) || x.user.UserName.Contains( request.UserName))
                 && (string.IsNullOrEmpty(request.UserRole) || x.role.Name == request.UserRole))
                .Select(result => new GetUserResponse
                {
                    Id = result.user.Id,
                    FirstName = result.user.FirstName,
                    LastName = result.user.LastName,
                    Username = result.user.UserName,
                    UserRole = result.role.Name,
                    Avatar=result.user.Avatar,
                    Email=result.user.Email,
                    Phone=result.user.PhoneNumber
                });
            await Task.CompletedTask;
            return result;
 
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

            var UserRole = !string.IsNullOrEmpty(request.UserRole) ? request.UserRole : "TECHNICIAN";

           // _applicationUser.Id = 1;

            var result = await _userManager.CreateAsync(_applicationUser, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_applicationUser, UserRole);
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
