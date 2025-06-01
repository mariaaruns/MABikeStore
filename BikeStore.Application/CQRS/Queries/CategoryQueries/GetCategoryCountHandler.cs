using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.CategoryResponse;

namespace BikeStore.Application.CQRS.Queries.CategoryQueries
{
    public record GetCategoryCountQuery() : IQuery<GetCategoryCountResponse>;
    public class GetCategoryCountHandler : IQueryHandler<GetCategoryCountQuery, GetCategoryCountResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetCategoryCountHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetCategoryCountResponse> Handle(GetCategoryCountQuery request, CancellationToken cancellationToken)
        {
            var GetCount = await _unitOfWork.CategoryRepository.GetCategoryCountAsync();
            if (GetCount != null)
            {
                return GetCount;
            }
            return new GetCategoryCountResponse
            {
                ActiveCategoriesCount = 0,
                InActiveCategoriesCount = 0
            };

        }
    }
}
