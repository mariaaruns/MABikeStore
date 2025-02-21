using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;

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

    public record CreateBrandcommand(CreateBrandRequest request):ICommand<CreateBrandResponse>;

    public class CreateBrandCommandHandler : ICommandHandler<CreateBrandcommand, CreateBrandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<CreateBrandResponse> Handle(CreateBrandcommand request, CancellationToken cancellationToken)
        {
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
