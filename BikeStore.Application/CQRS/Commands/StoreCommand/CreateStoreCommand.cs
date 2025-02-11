using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Request.StoreRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.DTO.Response.StoreResponse;
using BikeStore.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.StoreCommand
{

    public record CreateStoreCommand(CreateStoreRequest command):ICommand<CreateStoreResponse>;
    public class CreateStoreCommandHandler : ICommandHandler<CreateStoreCommand, CreateStoreResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStoreCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CreateStoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            var mappingStore = request.command.Adapt<Store>();
            
            var result = await _unitOfWork.StoreRepository.CreateAsync(mappingStore);
            await _unitOfWork.SaveAsync();
            if (result is null)
            {
                return default;
            }

            var MapResponse = result.Adapt<CreateStoreResponse>();
            return MapResponse;
        }
    }
}
