using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Abstraction.Messaging
{
    public interface IIdempotentCommand<out TResponse> :IRequest<TResponse>
    {
        Guid RequestId { get; set; }
    }
}
