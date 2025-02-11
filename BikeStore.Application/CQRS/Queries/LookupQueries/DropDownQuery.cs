using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Response.LookupResponse;
using FluentValidation.Validators;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.Domain.Models;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace BikeStore.Application.CQRS.Queries.LookupQueries
{

    public record GetDropdownQuery(string type) : IQuery<List<GetDropdownResponse>>;

    public class DropDownQueryHandler : IQueryHandler<GetDropdownQuery, List<GetDropdownResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DropDownQueryHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<List<GetDropdownResponse>> Handle(GetDropdownQuery request, CancellationToken cancellationToken)
        {
            switch (request.type.ToLower())
            {
                case "brand":

                    var result = await _unitOfWork.BrandRepository.GetAllAsync();
                    return result.Select(x => new GetDropdownResponse
                    {
                        Text = x.BrandName,
                        Value = x.BrandId
                    }).ToList();
                break;

                case "category":
                    var Catresult = await _unitOfWork.CategoryRepository.GetAllAsync();

                    return Catresult.Select(x => new GetDropdownResponse
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryId
                    }).ToList();
                break;

                default:
                    return default;

            }

        }
    }


    public class DropdownValidator : AbstractValidator<GetDropdownQuery>
    {
        public DropdownValidator()
        {
            RuleFor(x => x.type).NotEmpty();
        }


    }
}
