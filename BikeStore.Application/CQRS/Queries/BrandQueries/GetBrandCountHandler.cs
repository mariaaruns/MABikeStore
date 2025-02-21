using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.BrandResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.BrandQueries
{
    
    public record GetBrandCountQuery():IQuery<GetBrandCount>;

    public class GetBrandCountHandler : IQueryHandler<GetBrandCountQuery, GetBrandCount>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBrandCountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetBrandCount> Handle(GetBrandCountQuery request, CancellationToken cancellationToken)
        {
            var GetCount = await _unitOfWork.BrandRepository.GetBrandCountAsync();
            if (GetCount != null)
            {
                return GetCount;
            }
            return new GetBrandCount
            {
                ActiveBrandsCount = 0,
                InActiveBrandsCount = 0
            };

        }
    }
}
