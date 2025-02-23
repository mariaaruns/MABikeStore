using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BikeStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController :ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator) 
        {
            this._mediator = mediator;
        }
        
    }
}
