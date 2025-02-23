using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.UserCommand;
using BikeStore.Application.CQRS.Queries.UserQueries;
using BikeStore.Domain.DTO;
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

        [Authorize(Policy ="AdminRoles")]
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


        [HttpPost("GetAllUser")]
        public async Task<IActionResult> GetAllUser(GetUserRequest request)
        {

            var result = await _mediator.Send(new GetAllUserQuery(request));

            if (result is  null )
            {

                return Ok(ApiResponse<PaginationModel<GetUserResponse>>.Error(message: AppConstant.NOCONTENT, HttpStatusCode.NoContent));
            }
            else
            {
                return Ok(ApiResponse<PaginationModel<GetUserResponse>>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
            }
        }


        [HttpPost("AddRoleClaim")]
        public async Task<IActionResult> AddRoleClaimAsync(AddRoleClaimRequest request)
        {

            var result = await _mediator.Send(new AddRoleClaimCommand(request));

            if (result is { IsSuccess: false})
            {
                var erroList = result.ErrorDesc?.Split(",").ToList() ?? new List<string>();
                return Ok(ApiResponse<AddRoleClaimResponse>.Error(message: AppConstant.CRAETE_FAILED, HttpStatusCode.BadRequest,erroList));
            }
            else
            {
                return Ok(ApiResponse<AddRoleClaimResponse>.Success(message: AppConstant.CREATED_SUCCESS, HttpStatusCode.OK, result));
            }
        }


        [HttpPost("EditRoleClaim")]
        public async Task<IActionResult> EditRoleClaimAsync(EditRoleClaimRequest request)
        {

            var result = await _mediator.Send(new EditRoleClaimCommand(request));

            if (result is { IsSuccess: false })
            {

                return Ok(ApiResponse<EditRoleClaimResponse>.Error(message: AppConstant.UPDATE_FAILED, HttpStatusCode.BadRequest));
            }
            else
            {
                return Ok(ApiResponse<EditRoleClaimResponse>.Success(message: AppConstant.UPDATED_SUCCESS, HttpStatusCode.OK, result));
            }
        }


        [HttpPost("RemoveRoleClaim")]
        public async Task<IActionResult> DeleteRoleClaimAsync(AddRoleClaimRequest request)
        {

            var result = await _mediator.Send(new DeleteRoleClaimCommand(request));

            if (result is { IsSuccess: false })
            {

                return Ok(ApiResponse<AddRoleClaimResponse>.Error(message: AppConstant.CRAETE_FAILED, HttpStatusCode.BadRequest));
            }
            else
            {
                return Ok(ApiResponse<AddRoleClaimResponse>.Success(message: AppConstant.CREATED_SUCCESS, HttpStatusCode.OK, result));
            }
        }


        [HttpPost("ChangeUserRole")]
        public async Task<IActionResult> ChangeUserRoleAsync(ChangeUserRoleRequest request)
        {

            var result = await _mediator.Send(new ChangeUserRoleCommand(request));

            if (result is { IsSuccess: false })
            {
                var erroList = result.ErrorDesc?.Split(",").ToList() ?? new List<string>();
                return Ok(ApiResponse<ChangeUserRoleResponse>.Error(message: AppConstant.CRAETE_FAILED, HttpStatusCode.BadRequest, erroList));
            }
            else
            {
                return Ok(ApiResponse<ChangeUserRoleResponse>.Success(message: AppConstant.CREATED_SUCCESS, HttpStatusCode.OK, result));
            }
        }


    }
}
