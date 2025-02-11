using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Models;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Response.BrandResponse;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = BikeStore.Domain.Models;
using Mapster;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.Contracts.IService;

namespace BikeStore.Application.CQRS.Queries.BrandQueries
{

    public record GetBrandQuery(GetBrandRequest Request) : IQuery<PaginationModel<GetBrandResponse>>;

    public class GetBrandHandler : IQueryHandler<GetBrandQuery, PaginationModel<GetBrandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService<GetBrandResponse, Domain.Models.Brand> _paginationService;


        public GetBrandHandler(IUnitOfWork unitOfWork, IPaginationService<GetBrandResponse, Domain.Models.Brand> paginationService)
        {
            _unitOfWork = unitOfWork;
            _paginationService = paginationService;

        }
        public async Task<PaginationModel<GetBrandResponse>> Handle(GetBrandQuery query, CancellationToken cancellationToken)
        {
            var source = await _unitOfWork.BrandRepository.GetAllAsync();

            source = source.Where(x => 
            (string.IsNullOrEmpty(query.Request.BrandFilter)|| x.BrandName.Contains(query.Request.BrandFilter))
            &&
            (query.Request.IsActive==null || x.IsActive== query.Request.IsActive)
            );

        /*    if (query.Request.IsActive != true)
            { 
            
            }
            if (query.Request.BrandFilter != null)
            {
                source = source.Where(x => x.BrandName.Contains(query.Request.BrandFilter));
            }*/
            if (query.Request.SortOption != null)
            {
                source = query.Request.SortOption.Equals("Desc", StringComparison.OrdinalIgnoreCase) ? source.OrderByDescending(x => x.BrandName) : source.OrderBy(x => x.BrandName);
            }

            var result = _paginationService.Pagination(source, query.Request);

            return result;
        }
    }
}
