using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.DTO.Response.ProductResponse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.ProductQueries
{

    public record GetProductCountQuery():IQuery<GetProductCountResponse>;
    public class GetProductCountQueryHandler : IQueryHandler<GetProductCountQuery, GetProductCountResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetProductCountResponse> Handle(GetProductCountQuery request, CancellationToken cancellationToken)
        {
            var GetCount = await _unitOfWork.ProductRepository.GetProductCountAsync();
            if (GetCount != null)
            {
                return GetCount;
            }
            return new GetProductCountResponse
            {
                ActiveProductCount = 0,
                InactiveProductCount = 0
            };
        }
    }
}
