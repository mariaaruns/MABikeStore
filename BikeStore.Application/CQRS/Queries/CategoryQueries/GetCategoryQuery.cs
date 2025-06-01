using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.CategoryQueries
{
    public record GetCategoryQuery(GetCategoryRequest request) : IQuery<PaginationModel<GetCategoryResposne>>;
    public class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, PaginationModel<GetCategoryResposne>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService<GetCategoryResposne, Category> _paginationService;
        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IPaginationService<GetCategoryResposne, Category> paginationService)
        {
            _paginationService = paginationService;
            this._unitOfWork = unitOfWork;
        }
        public async Task<PaginationModel<GetCategoryResposne>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
        {
            var source = await _unitOfWork.CategoryRepository.GetAllAsync();
            source = source.Where(
                x => (string.IsNullOrEmpty(query.request.CategoryNameFilter) || x.CategoryName.Contains(query.request.CategoryNameFilter))
                && x.IsActive==query.request.IsActive
                
            );

            var result = _paginationService.Pagination(source, query.request);
            return result;
        }
    }
}
