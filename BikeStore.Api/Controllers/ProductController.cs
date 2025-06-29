﻿using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Queries.CategoryQueries;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MediatR;
using BikeStore.Domain.DTO.Request.ProductRequest;
using BikeStore.Domain.DTO.Response.ProductResponse;
using BikeStore.Application.CQRS.Queries.ProductQueries;
using Microsoft.AspNetCore.Hosting;
using BikeStore.Application.CQRS.Commands.ProductCommand;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure.Core;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]

    public class ProductController(IMediator mediator,IWebHostEnvironment enviroment) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IWebHostEnvironment _enviroment = enviroment;

        [HttpPost]
        public async Task<IActionResult> GetAllProductsAsync([FromBody] GetProductRequest request)
        {
            var result = await _mediator.Send(new GetProductQuery(request));
            if (result is null)
            {
                return Ok(ApiResponse<PaginationModel<GetProductResponse>>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.OK));
            }

            return Ok(ApiResponse<PaginationModel<GetProductResponse>>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProductAsync( [FromForm] CreateProductRequest request)
        {
            var uploadsFolderPath = Path.Combine(_enviroment.WebRootPath, "Assets", "Uploads", "Products");
            var result = await _mediator.Send(new CreateProductCommand(request, uploadsFolderPath));
        
            if (result is null) {

                return Ok(ApiResponse<GetProductResponse>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.BadRequest));
            }

            return Ok(ApiResponse<GetProductResponse>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateProductAsync(int Id,[FromForm]UpdateProductRequest request) 
        {
            if (Id != request.ProductId) 
            {
                return Ok(ApiResponse<GetProductResponse>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.BadRequest));
            }
            var uploadsFolderPath = Path.Combine(_enviroment.WebRootPath, "Assets", "Uploads","Products");
            
            var result = await _mediator.Send(new UpdateProductCommand(request,uploadsFolderPath));
         
            if (result is null)
            {
                return Ok(ApiResponse<GetProductResponse>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.BadRequest));
            }
            return Ok(ApiResponse<GetProductResponse>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetProductDetailByIdAsync(int Id)
        {
            if (Id <= 0) {
                return Ok(ApiResponse<PaginationModel<GetProductResponse>>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.BadRequest));
            }
            var result = await _mediator.Send(new GetProductDetailQuery(Id));
            if (result is null)
            {
                return Ok(ApiResponse<GetProductResponse>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.OK));
            }

            return Ok(ApiResponse<GetProductResponse>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteProductByIdAsync(int Id)
        {
            if (Id <= 0)
            {
                return Ok(ApiResponse<bool>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.BadRequest));
            }
            var result = await _mediator.Send(new DeleteProductCommand(Id));
            
            if (result is false)
            {
                return Ok(ApiResponse<bool>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.OK));
            }

            return Ok(ApiResponse<bool>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }

        [HttpGet("GetProductCount")]
        public async Task<IActionResult> GetProductCountAsync() 
        {
            var result = await _mediator.Send(new GetProductCountQuery());
            if (result is null)
            {
                return Ok(ApiResponse<GetProductCountResponse>.Error(message: AppConstant.RECORDS_NOT_FOUND, HttpStatusCode.OK));
            }

            return Ok(ApiResponse<GetProductCountResponse>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
        }
    }
}
