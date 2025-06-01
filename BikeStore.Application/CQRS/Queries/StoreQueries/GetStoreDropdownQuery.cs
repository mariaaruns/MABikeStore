using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Application.Common;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.LookupResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Queries.StoreQueries
{
    public record GetStoreDropdownQuery:IQuery<List<GetDropdownResponse>>;
    
    public class GetStoreDropdownQueryHandler : IQueryHandler<GetStoreDropdownQuery, List<GetDropdownResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStoreDropdownQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<List<GetDropdownResponse>> Handle(GetStoreDropdownQuery request, CancellationToken cancellationToken)
        {
            var getStore = await _unitOfWork.StoreRepository.GetAllAsync();

            var dropdownFields = getStore.Select(x=> new GetDropdownResponse 
            { 
                Text=x.StoreName,
                Value=x.StoreId            
            }).ToList();

            if (!dropdownFields.Any()) 
            {
                throw new FluentValidation.ValidationException(AppConstant.RECORDS_NOT_FOUND);
            }

            return dropdownFields;
        }
    }

}
