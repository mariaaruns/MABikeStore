using BikeStore.Application.APIResponse;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Queries.LookupQueries;
using BikeStore.Domain.DTO.Response.LookupResponse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LookupController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("Dropdown")]
        public async Task<IActionResult> Dropdown(string type)
        {
            var result = await _mediator.Send(new GetDropdownQuery(type));
            if (result==null) {

                return Ok(ApiResponse<List<GetDropdownResponse>>.Error(AppConstant.BADREQUEST, System.Net.HttpStatusCode.BadRequest));
            }
            return Ok(ApiResponse<List<GetDropdownResponse>>.Success(AppConstant.SUCCESS,System.Net.HttpStatusCode.OK,result));

        }
    }
}
