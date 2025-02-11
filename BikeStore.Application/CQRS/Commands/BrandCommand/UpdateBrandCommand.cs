using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.Models;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.BrandCommand
{
    public record UpdateBrandCommand(UpdateBrandRequest Command):ICommand<CreateBrandResponse>;
    public class UpdateBrandHandler : ICommandHandler<UpdateBrandCommand, CreateBrandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBrandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CreateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand getBrand = await _unitOfWork.BrandRepository.GetByIdAsync(x => x.BrandId == request.Command.BrandId);
            getBrand.BrandName = request.Command.BrandName;
            getBrand.Logo = request.Command.Logo;

            var result = await _unitOfWork.BrandRepository.UpdateExistingBrandAsync(getBrand);
            var IsSuccess = await _unitOfWork.SaveAsync();
            if (IsSuccess)
            {
                var response = result.Adapt<CreateBrandResponse>();
                return response;
            }
            else {
                return default;
            }
        }
    }
}
