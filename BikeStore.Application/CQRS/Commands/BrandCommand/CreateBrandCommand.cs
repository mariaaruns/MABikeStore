using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO.Request.BrandRequest;
using BikeStore.Domain.DTO.Response.BrandResponse;
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
            if (request.request.LogoImage!=null) 
            {
                var fileaPath = await _fileService.SaveFileAsync(request.request.LogoImage,request.ImagePath);
                request.request.Logo = fileaPath;
            }
            var MappingBrand = request.request.Adapt<Brands.Brand>();
            var Entity=await unitOfWork.BrandRepository.CreateAsync(MappingBrand);
            await unitOfWork.SaveAsync();

            var MapResponse = Entity.Adapt<CreateBrandResponse>();
            return MapResponse;
        }
    }


    //fluent Validation
    public class CreateBrandValidator : AbstractValidator<CreateBrandcommand> 
    {
        public CreateBrandValidator()
        {
            RuleFor(x => x.request.BrandName).NotEmpty();
            //RuleFor(x => x.request.Logo).NotEmpty();
        }
    
    }

}
