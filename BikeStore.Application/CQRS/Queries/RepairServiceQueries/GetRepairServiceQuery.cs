using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.RepairServiceRequest;
using BikeStore.Domain.DTO.Response.RepairServiceResposne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.RepairServiceQueries
{
    public record GetAllRepairQuery(GetRepairServiceRequest Request):IQuery<PaginationModel<GetRepairServiceResponse>>;

    public class GetRepairServiceQueryHandler 
        : IQueryHandler<GetAllRepairQuery, PaginationModel<GetRepairServiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaginationService<GetRepairServiceResponse, GetRepairServiceResponse> _paginationService;

        public GetRepairServiceQueryHandler(IUnitOfWork unitOfWork,IPaginationService<GetRepairServiceResponse, GetRepairServiceResponse> paginationService)
        {
            _unitOfWork = unitOfWork;
            _paginationService = paginationService;
        }
        public async Task<PaginationModel<GetRepairServiceResponse>> Handle(GetAllRepairQuery request, CancellationToken cancellationToken)
        {
            var result =await _unitOfWork.repairServiceRepository.GetallRepairServiceAsync(request.Request);
            return _paginationService.Pagination(result, request.Request);
        }
    }

}
