using Azure.Core;
using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.BrandCommand;

using BikeStore.Application.CQRS.Queries.BrandQueries;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IMediator _mediator;
        private readonly IWebHostEnvironment _environment;

        public BrandController(IMediator mediator,IWebHostEnvironment environment)
        {
            _mediator = mediator;
            this._environment = environment;
        }


        [HttpPost]
        public async Task<IActionResult> GetAllBrands([FromBody] GetBrandRequest request) 
        {
            var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "Assets", "Uploads", "Brands");
            var brandList = await _mediator.Send(new GetBrandQuery(request,uploadsFolderPath));
            if (brandList is null) 
            {
                return Ok(ApiResponse<GetBrandResponse>.Error("No Records found", HttpStatusCode.BadRequest, null, null));
            }

            return Ok(ApiResponse<PaginationModel<GetBrandResponse>>.Success("Records Fetched", HttpStatusCode.OK, brandList, null));
        }


        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetBrandById(int Id)
        {

            if (Id > 0)
            {
                var result = await _mediator.Send(new GetBrandByIdQuery(Id));
                if (result is not { })
                    return Ok(ApiResponse<GetBrandResponse>.Success("Brand Id Fetched", HttpStatusCode.OK, result, null));

                else {
                    return Ok(ApiResponse<GetBrandResponse>.Success("No records found", HttpStatusCode.OK, result, null));
                }
            }
            else {
                
                return Ok(ApiResponse<GetBrandResponse>.Error("Invalid Brand Id", HttpStatusCode.BadRequest, null, null));
            }
        }

        [HttpGet("GetBrandCount")]
        public async Task<IActionResult> GetBrandCount()
        {

            var brandCount = await _mediator.Send(new GetBrandCountQuery());
            if (brandCount is null)
            {
                return Ok(ApiResponse<GetBrandCount>.Error("No Records found", HttpStatusCode.BadRequest, null, null));
            }

            return Ok(ApiResponse<GetBrandCount>.Success("Records Fetched", HttpStatusCode.OK, brandCount, null));

        }
        
        [HttpPost("CreateBrand")]
        public async Task<IActionResult> AddNewBrand([FromBody] CreateBrandRequest request) 
        {
            var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "Assets", "Uploads", "Brands");
            var result = await _mediator.Send(new CreateBrandcommand(request,uploadsFolderPath));
            if (result != null)
            {
                return Ok(ApiResponse<CreateBrandResponse>.Success(message: AppConstant.CREATED_SUCCESS, HttpStatusCode.OK, result));
            }
            else
            {
                return Ok(ApiResponse<CreateBrandResponse>.Error(message: AppConstant.CRAETE_FAILED, HttpStatusCode.BadRequest));
            }
        }

        [HttpPut("updatebrand")]
        public async Task<IActionResult> UpdateExistingBrand([FromBody] UpdateBrandRequest request)
        {
            var uploadsFolderPath = Path.Combine(_environment.ContentRootPath, "Assets", "Uploads", "Brands");
            
            var result = await _mediator.Send(new UpdateBrandCommand(request, uploadsFolderPath));

            if (result is null)
            {
                return Ok(ApiResponse<CreateBrandResponse>.Error(message: AppConstant.UPDATE_FAILED, HttpStatusCode.BadRequest));
            }
            else
            {
                return Ok(ApiResponse<CreateBrandResponse>.Success(message: AppConstant.CREATED_SUCCESS, HttpStatusCode.OK, result));
            }          
        }

        [HttpDelete("DeleteBrand")]
        public async Task<IActionResult> RemoveBrandById([FromQuery] int id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand(id));
            
            if (result!=true) 
            {
                return Ok(ApiResponse<bool>.Error(message: AppConstant.DELETE_FAILED, HttpStatusCode.BadRequest));
            }

            return Ok(ApiResponse<bool>.Success(message: AppConstant.DELETED_SUCCESS, HttpStatusCode.OK,result));

        }




    }
}
