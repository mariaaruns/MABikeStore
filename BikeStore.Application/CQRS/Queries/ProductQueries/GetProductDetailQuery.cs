using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.DTO.Response.ProductResponse;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.ProductQueries
{

    public record GetProductDetailQuery(int id):IQuery<GetProductResponse>;

    public class GetProductDetailQueryHandler : IQueryHandler<GetProductDetailQuery, GetProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetProductDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<GetProductResponse> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.id is 0 or < 0)
            {
                return null;
            }
            
            var result = await _unitOfWork.ProductRepository.GetByIdAsync(x => x.ProductId == request.id);
            
            if (result is null) 
            {
                return null;
            }
            var Response=result.Adapt<GetProductResponse>();
            return Response;
        }
    }
}
