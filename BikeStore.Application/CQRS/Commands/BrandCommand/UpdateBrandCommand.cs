using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.Models;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.BrandCommand
{
    public record UpdateBrandCommand(UpdateBrandRequest Command,string ImagePath):ICommand<CreateBrandResponse>;
    public class UpdateBrandHandler : ICommandHandler<UpdateBrandCommand, CreateBrandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileservice;

        public UpdateBrandHandler(IUnitOfWork unitOfWork, IFileService fileservice)
        {
            this._unitOfWork = unitOfWork;
            this._fileservice = fileservice;
        }
        public async Task<CreateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand getBrand = await _unitOfWork.BrandRepository.GetByIdAsync(x => x.BrandId == request.Command.BrandId);
              if (request.Command.FileName!=null && request.Command.ImageBytes!=null) 
              {
                  if (!string.IsNullOrEmpty(getBrand.Logo))
                  {
                   bool isSuccess= await _fileservice.DeleteFileAsync(Path.Combine(request.ImagePath, getBrand.Logo));

                  }
                  var filePath = await _fileservice.SaveFileAsync(request.Command.ImageBytes,request.Command.FileName,request.ImagePath);
                  
                getBrand.Logo = filePath;
              }
            getBrand.BrandName = request.Command.BrandName;            
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
