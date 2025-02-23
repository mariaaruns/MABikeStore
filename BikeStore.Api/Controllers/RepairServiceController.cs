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
    }
}
