using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.StockCommand;
using BikeStore.Application.CQRS.Queries.StockQueries;
using BikeStore.Application.CQRS.Queries.StoreQueries;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.Stock;
using BikeStore.Domain.DTO.Response.Stock;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAllStockDetails")]
        public async Task<IActionResult> GetAllStockAsync(GetStockRequest req)
        {
            var result = await _mediator.Send(new GetAllStockQuery(req));

            if (result is null) {

               return Ok (ApiResponse<PaginationModel<GetStockResponse>>.Error(AppConstant.BADREQUEST,System.Net.HttpStatusCode.BadRequest));
            }

            return Ok(ApiResponse<PaginationModel<GetStockResponse>>.Success(AppConstant.SUCCESS, System.Net.HttpStatusCode.OK, result));
        }


        [HttpPost("UpdateStockQty")]
        public async Task<IActionResult> UpdateStockQty(UpsertStockRequest request) 
        {
            var result = await _mediator.Send(new UpdateStockCommand(request));

            if (result is null)
            {

                return Ok(ApiResponse<object>.Error(AppConstant.BADREQUEST, System.Net.HttpStatusCode.BadRequest));
            }

            return Ok(ApiResponse<GetStockResponse>.Success(AppConstant.SUCCESS, System.Net.HttpStatusCode.OK, result));
        }

    }
}
