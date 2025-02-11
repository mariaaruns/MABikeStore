using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IRepository
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(UserRegisterRequest request);
        
        Task<LoginResponse> LoginAsync(LoginRequest request);
        
        Task<dynamic> GenerateJWtTokenAsync();
        Task LogoutAsync();
    }
}
