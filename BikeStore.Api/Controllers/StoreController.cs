﻿using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.StoreCommand;
using BikeStore.Application.CQRS.Queries.StoreQueries;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.LookupResponse;
using BikeStore.Domain.DTO.Response.StoreResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAllStoreAsync([FromBody]GetStoreRequest request)
        {

            var result = await _mediator.Send(new GetStoreQuery(request));

            if (result.Items != null)
            {
                return Ok(ApiResponse<PaginationModel<GetStoreResponse>>.Success(message: "success", HttpStatusCode.OK, result));
            }
            else {

                return Ok(ApiResponse<PaginationModel<GetStoreResponse>>.Error(message:"success",HttpStatusCode.BadRequest));
            }
       
        }

        [HttpPost("CreateStore")]
        public async Task<IActionResult> AddNewStoreAsync(CreateStoreRequest request)
        {
            var result = await _mediator.Send(new CreateStoreCommand(request));
            if (result != null)
            {
                return Ok(ApiResponse<CreateStoreResponse>.Success(message: AppConstant.CREATED_SUCCESS, HttpStatusCode.OK, result));
            }
            else
            {
                return Ok(ApiResponse<CreateStoreResponse>.Error(message: AppConstant.CRAETE_FAILED, HttpStatusCode.BadRequest));
            }

        }

        [HttpPut("UpdateStore/{id:int}")]
        public async Task<IActionResult> UpdateStoreAsync(int id,[FromBody]UpdateStoreRequest request)
        {
            var result = await _mediator.Send(new UpdateStoreCommand(request));
            if (result != null)
            {
                return Ok(ApiResponse<UpdateStoreResponse>.Success(message: AppConstant.UPDATED_SUCCESS, HttpStatusCode.OK, result));
            }
            else
            {
                return Ok(ApiResponse<UpdateStoreResponse>.Error(message: AppConstant.UPDATE_FAILED, HttpStatusCode.BadRequest));
            }

        }

        [HttpGet("GetStoreDropdown")]
        public async Task<IActionResult> GetStoreDropdownAsync()
        {
            var result = await _mediator.Send(new GetStoreDropdownQuery());

            if (result != null)
            {
                return Ok(ApiResponse<List<GetDropdownResponse>>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
            }
            else
            {
                return Ok(ApiResponse<List<GetDropdownResponse>>.Error(message: AppConstant.BADREQUEST, HttpStatusCode.BadRequest));
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStoreByIdAsync(int id)
        {
            var result = await _mediator.Send(new GetStoreByIdQuery(id));
            if (result != null)
            {
                return Ok(ApiResponse<UpdateStoreResponse>.Success(message: AppConstant.SUCCESS, HttpStatusCode.OK, result));
            }
            else
            {
                return Ok(ApiResponse<UpdateStoreResponse>.Error(message: AppConstant.UPDATE_FAILED, HttpStatusCode.BadRequest));
            }

        }

        [HttpDelete("DeleteStore/{id:int}")]
        public async Task<IActionResult> DeleteStoreAsync(int id)
        {
            var result = await _mediator.Send(new DeleteStoreCommand(id));
            if (result)
            {
                return Ok(ApiResponse<bool>.Success(message: AppConstant.DELETED_SUCCESS, HttpStatusCode.OK, result));
            }
            else
            {
                return Ok(ApiResponse<bool>.Error(message: AppConstant.DELETE_FAILED, HttpStatusCode.BadRequest));
            }

        }

    }
}
