using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.BrandResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.BrandQueries
{

    public record GetBrandByIdQuery(int id) : IQuery<GetBrandResponse>;
    public class GetBrandByIdHandler : IQueryHandler<GetBrandByIdQuery, GetBrandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBrandByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetBrandResponse> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.BrandRepository.GetByIdAsync(x => x.BrandId == request.id);
            
            if (result is null)
            {
             return new GetBrandResponse();
            }

            return new GetBrandResponse
            {
                BrandId = result.BrandId,
                BrandName = result.BrandName,
                Logo = result.Logo
            };
        }
    }
}
