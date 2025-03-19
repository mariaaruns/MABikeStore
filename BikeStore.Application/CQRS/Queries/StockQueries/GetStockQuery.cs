using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Application.CQRS.Queries.UserQueries;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.Stock;
using BikeStore.Domain.DTO.Response.Stock;
using BikeStore.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.StockQueries
{
    public record GetAllStockQuery(GetStockRequest req):IQuery<PaginationModel<GetStockResponse>>;

    public class GetStockQueryHandler : IQueryHandler<GetAllStockQuery, PaginationModel<GetStockResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService<GetStockResponse, GetStockResponse> _paginationService;

        
        public GetStockQueryHandler(IUnitOfWork unitOfWork,IPaginationService<GetStockResponse, GetStockResponse> paginationService)
        {
            this._unitOfWork = unitOfWork;
            this._paginationService = paginationService;
        }
        public async Task<PaginationModel<GetStockResponse>> Handle(GetAllStockQuery request, CancellationToken cancellationToken)
        {
            if (request.req.StoreId == 0) {
                return default;
            }

            var GetStockList = await _unitOfWork.stockRepository.GetAllStockAsync();

            var result = GetStockList
                        .Where(x =>
                        (x.StoreId == request.req.StoreId)
                        && (string.IsNullOrEmpty(request.req.ProductName) || x.Product.ProductName.Contains(request.req.ProductName))
                        && (request.req.QuantityLessThan == 0 || x.Quantity <= request.req.QuantityLessThan)
                        ).Select(x=>new GetStockResponse { 
                        
                        ProductId=x.ProductId,
                        ProductName=x.Product.ProductName,
                        Quantity=x.Quantity,
                        StoreId=x.StoreId,
                        StoreName=x.Store.StoreName
                        });

            var response = _paginationService.Pagination(result,request.req);

            return response;

        }
    }
}
