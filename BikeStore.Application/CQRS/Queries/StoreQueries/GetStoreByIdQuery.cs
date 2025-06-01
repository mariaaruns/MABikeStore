using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO.Response.StoreResponse;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.StoreQueries
{
    public record GetStoreByIdQuery(int id):IQuery<UpdateStoreResponse>;

    public class GetStoreByIdQueryHandler : IQueryHandler<GetStoreByIdQuery, UpdateStoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStoreByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }        
        public async Task<UpdateStoreResponse> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.StoreRepository.GetByIdAsync(x => x.StoreId== request.id);
            
            if (result is null)
            {
                throw new FluentValidation.ValidationException("invalid store id");
            }

            var mapEntityToResponse = result.Adapt<UpdateStoreResponse>();
            
            return mapEntityToResponse;
        }
    }
}
