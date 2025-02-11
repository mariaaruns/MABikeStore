using BikeStore.Application.APIResponse;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;

namespace Bikestore.Client.Services.Interface
{
    public interface IAuthenticationService
    {

        Task<ApiResponse<RegisterResponse>> RegisterUser(UserRegisterRequest userForRegistration);
        Task<ApiResponse<LoginResponse>> Login(LoginRequest userForAuthentication);
        Task Logout();
    }
}
