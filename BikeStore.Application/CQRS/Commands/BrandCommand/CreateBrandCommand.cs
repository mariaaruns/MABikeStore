using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
using BikeStore.Domain.Models;
using FluentValidation;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brands = BikeStore.Domain.Models;
namespace BikeStore.Application.CQRS.Commands.BrandCommand
{

    public record CreateBrandcommand(CreateBrandRequest request,string ImagePath):ICommand<CreateBrandResponse>;

    public class CreateBrandCommandHandler : ICommandHandler<CreateBrandcommand, CreateBrandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService _fileService;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork,IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this._fileService = fileService;
        }

       
        public async Task<CreateBrandResponse> Handle(CreateBrandcommand request, CancellationToken cancellationToken)
        {
            var brand = new Brand();
            if (request.request.ImageBytes!=null && !string.IsNullOrEmpty(request.request.FileName)) 
            {
                var filePath = await _fileService.SaveFileAsync(request.request.ImageBytes, request.request.FileName, request.ImagePath);
                brand.Logo = filePath;
            }

            brand.BrandName = request.request.BrandName;
            brand.IsActive = true;
            var Entity=await unitOfWork.BrandRepository.CreateAsync(brand);
            await unitOfWork.SaveAsync();
            var MapResponse = Entity.Adapt<CreateBrandResponse>();
            return MapResponse;
        }

       
    }


    //fluent Validation
    public class CreateBrandValidator : AbstractValidator<CreateBrandcommand> 
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateBrandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.request.BrandName)
                .NotEmpty().MustAsync(BrandNameUnique).WithMessage("Brand name already exists");
            
        }

        private async Task<bool> BrandNameUnique(string brandName, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.BrandRepository.ExistAsync(b => b.BrandName == brandName);
            return !exists;
        }

    }

}
