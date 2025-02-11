using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.CategoryCommand;
using BikeStore.Application.CQRS.Queries.CategoryQueries;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("Category")]
        public async Task<IActionResult> GetAllCategories([FromQuery ]GetCategoryRequest request)
        {
            var result = await _mediator.Send(new GetCategoryQuery(request));
            if (result is null)
            {
                return Ok(ApiResponse<PaginationModel<GetCategoryResposne>>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.OK));
            }

            return Ok(ApiResponse<PaginationModel<GetCategoryResposne>>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpPost("Category")]
        public async Task<IActionResult> AddCategory(CreateCategoryRequest request)
        {
            var result = await _mediator.Send(new CreateCategoryCommand(request));
            
            if (result is null)
            {
                return Ok(ApiResponse<GetCategoryResposne>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.OK));
            }

            return Ok(ApiResponse<GetCategoryResposne>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryRequest request)
        {
            if (id != request.CategoryId)
            {
                return Ok(ApiResponse<GetCategoryResposne>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.OK));
            }
 
            var result = await _mediator.Send(new UpdateCategoryCommand(request));

            if (result is null)
            {
                return Ok(ApiResponse<GetCategoryResposne>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.OK));
         
            }

            return Ok(ApiResponse<GetCategoryResposne>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id != 0)
            {
                return Ok(ApiResponse<GetCategoryResposne>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.OK));
            }
            var IsSuccess = await _mediator.Send(new DeleteCategoryCommand(id));

            if (!IsSuccess)
            {
                return Ok(ApiResponse<GetCategoryResposne>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.OK));
            }
            return Ok(ApiResponse<GetCategoryResposne>.Success(message: AppConstant.DELETED_SUCCESS, HttpStatusCode.OK));
        }
    }
}
