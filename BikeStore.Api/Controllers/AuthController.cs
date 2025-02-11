using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.UserCommand;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("Login")]
         public async Task<IActionResult> LoginAsync(LoginRequest request) {

            var result = await _mediator.Send(new LoginCommand(request));

            if (result is { Token: null })
            {

                return Ok(ApiResponse<LoginResponse>.Error(message:"Login failed",HttpStatusCode.BadRequest));
            }
            else 
            { 
                return Ok(ApiResponse<LoginResponse>.Success(message:"Login Success",HttpStatusCode.OK,result));
            }
         }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(UserRegisterRequest request)
        {
          //  request.Avatar = "DefaultAvatar.jpg";

            var result = await _mediator.Send(new RegisterUserCommand(request));
            if (result.IsSuccess is false)
            {
                return Ok(ApiResponse<RegisterResponse>.Error(message: "User Register failed", System.Net.HttpStatusCode.BadRequest, result.Errors));
            }
            return Ok(ApiResponse<RegisterResponse>.Success(message: "User Register Successfully", System.Net.HttpStatusCode.BadRequest));
        }
        /*
        [HttpPost]
        public Task<IActionResult> LogoutAsync(LoginRequest request)
        {


        }


        [Authorize]
        [HttpGet]
        public Task<IActionResult> GetRolesAsync(LoginRequest request)
        {


        }
*/




    }
}
