using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.ProductRequest;
using BikeStore.Domain.DTO.Response.ProductResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Queries.ProductQueries
{

    public record GetProductQuery(GetProductRequest request):ICommand<PaginationModel<GetProductResponse>>;

    public class GetProductQueryHandler : ICommandHandler<GetProductQuery, PaginationModel<GetProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService<GetProductResponse,GetProductResponse> _pagiantion;
        public GetProductQueryHandler(IUnitOfWork unitOfWork, IPaginationService<GetProductResponse, GetProductResponse> paginationService)
        {
            _unitOfWork = unitOfWork;
            _pagiantion = paginationService;
        }
        

        public async Task<PaginationModel<GetProductResponse>> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var ProductResult = await _unitOfWork.ProductRepository.GetProductListAsync();
         
            var responses = ProductResult.Where(x=>
            
            (string.IsNullOrEmpty(query.request.ProductFilter) ||x.ProductName.Contains(query.request.ProductFilter))
            &&
            ( !query.request.BrandFilter.HasValue || x.BrandId==query.request.BrandFilter.Value)
            &&
            (!query.request.CategoryFilter.HasValue || x.CategoryId == query.request.CategoryFilter.Value)
            ).Select(x => new GetProductResponse
            {
                ProductId = x.ProductId,
                ProductName=x.ProductName,
                ListPrice=x.ListPrice,
                Brand=x.Brand.BrandName,
                Category=x.Category.CategoryName,
                ModelYear=x.ModelYear,
                Image=x.Image
            });

            return _pagiantion.Pagination(responses,query.request);
        }
    }
}
