using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.UserQueries
{
    public record GetAllUserQuery(GetUserRequest request):IQuery<PaginationModel<GetUserResponse>>;
    public class GetAllUserQueryHandler : IQueryHandler<GetAllUserQuery, PaginationModel<GetUserResponse>>
    {
        private readonly IAuthenticationService _authService;
        private readonly IPaginationService<GetUserResponse,GetUserResponse> _paginationService;

        public GetAllUserQueryHandler(IAuthenticationService authenticationService,IPaginationService<GetUserResponse, GetUserResponse> paginationService)
        {
            this._authService = authenticationService;
            this._paginationService = paginationService;
        }
        public async Task<PaginationModel<GetUserResponse>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var GetAllUser = await _authService.GetAllUser(request.request);

            var result =  _paginationService.Pagination(GetAllUser,request.request);
            
            return result;

        }
    }
}
