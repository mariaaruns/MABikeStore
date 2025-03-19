using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.RepairServiceCommand;
using BikeStore.Application.CQRS.Queries.RepairServiceQueries;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.RepairServiceRequest;
using BikeStore.Domain.DTO.Response.RepairServiceResposne;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairServiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RepairServiceController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("GetAllRepairService")]
        public async Task<IActionResult> GetAllRepairService(GetRepairServiceRequest request) 
        {

            var result = await _mediator.Send(new GetAllRepairQuery(request));

            if (result is null) 
            {
                return Ok(ApiResponse<PaginationModel<GetRepairServiceResponse>>.Error(AppConstant.BADREQUEST,System.Net.HttpStatusCode.BadRequest));
            }
            return Ok(ApiResponse<PaginationModel<GetRepairServiceResponse>>.Success(AppConstant.SUCCESS, System.Net.HttpStatusCode.OK,result));
        }


        [HttpPost("CreateRepairService")]
        public async Task<IActionResult> CreateRepairService(AddNewRepairRequest request)
        {

            var result = await _mediator.Send(new CreateRepairServiceCommand(request));

            if (result is null)
            {
                return Ok(ApiResponse<CreateRepairServiceResponse>.Error(AppConstant.BADREQUEST, System.Net.HttpStatusCode.BadRequest));
            }
            return Ok(ApiResponse<CreateRepairServiceResponse>.Success(AppConstant.SUCCESS, System.Net.HttpStatusCode.OK,data:result));        
        }

    }
}
