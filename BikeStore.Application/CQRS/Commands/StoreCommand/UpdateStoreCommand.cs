using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Application.Common;
using BikeStore.Application.CQRS.Commands.BrandCommand;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.DTO.Response.StoreResponse;
using BikeStore.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.StoreCommand
{
    public record UpdateStoreCommand(UpdateStoreRequest req):ICommand<UpdateStoreResponse>;
    public class UpdateStoreCommandHandler : ICommandHandler<UpdateStoreCommand, UpdateStoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStoreCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<UpdateStoreResponse> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var getStore = await _unitOfWork.StoreRepository.GetByIdAsync(x => x.StoreId == request.req.StoreId);

            if (getStore is null)
            {
                throw new FluentValidation.ValidationException(AppConstant.INVALID_INPUT);
            }

            var mapReqToEntity = getStore.Adapt<Store>();

            var response = await _unitOfWork.StoreRepository.UpdateStoreAsync(mapReqToEntity);
            var isSuccess = await _unitOfWork.SaveAsync();

            if (response == null || !isSuccess)
            {
                throw new FluentValidation.ValidationException(AppConstant.UPDATE_FAILED);    
            }
            
            return response.Adapt<UpdateStoreResponse>();
            
        }
    }
}
