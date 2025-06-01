using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.StoreResponse;
using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.StoreQueries
{
    public record GetStoreQuery(GetStoreRequest query) : IQuery<PaginationModel<GetStoreResponse>>;
    public class GetStoreQueryHandler : IQueryHandler<GetStoreQuery, PaginationModel<GetStoreResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService<GetStoreResponse, GetStoreResponse> _paginationService;
        public GetStoreQueryHandler(IUnitOfWork unitOfWork, IPaginationService<GetStoreResponse, GetStoreResponse> paginationService)
        {
            this._unitOfWork = unitOfWork;
            _paginationService = paginationService;
        }
        public async Task<PaginationModel<GetStoreResponse>> Handle(GetStoreQuery request, CancellationToken cancellationToken)
        {
            var source = await _unitOfWork.StoreRepository.GetAllAsync();

           var GetStores = source.Where(
                x => 
                (
                    string.IsNullOrEmpty(request.query.StoreNameFilter)
                    || x.StoreName.Contains(request.query.StoreNameFilter)
                )
            ).Select(x=> new GetStoreResponse() { 
            StoreId=x.StoreId,
            StoreName=x.StoreName,
            Email=x.Email,
            Phone=x.Phone,
            Address= $"{x.Street},{x.City},{x.State},{x.ZipCode}"
            });

            var result = _paginationService.Pagination(GetStores, request.query);
            return result;

        }
    }
}
