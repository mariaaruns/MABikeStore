using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using BikeStore.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.CategoryCommand
{

    public record CreateCategoryCommand(CreateCategoryRequest command): ICommand<GetCategoryResposne>;
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, GetCategoryResposne>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<GetCategoryResposne> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            
            var Entity = command.command.Adapt<Category>();
            
            var result = await _unitOfWork.CategoryRepository.CreateAsync(Entity);
            var IsCreated = await _unitOfWork.SaveAsync();
            if (IsCreated)
            {
                var response = result.Adapt<GetCategoryResposne>();
                return response;
            }
            else 
            {
                return default;
            }
            
        }
    }
}
