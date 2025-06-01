using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.CategoryRequest;
using BikeStore.Domain.DTO.Response.CategoryResponse;
using Mapster;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.CategoryCommand
{
    public record UpdateCategoryCommand(UpdateCategoryRequest request):ICommand<GetCategoryResposne>;
    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, GetCategoryResposne>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<GetCategoryResposne> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var getCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(x => x.CategoryId == command.request.CategoryId);

            if (getCategory is null) {
                return default;
            }

            getCategory.CategoryName = command.request.CategoryName;
            
            var response = await _unitOfWork.CategoryRepository.UpdateCategoryAsync(getCategory);
            var isSuccess=await _unitOfWork.SaveAsync();
               
            if (response != null && isSuccess) 
                {
                    return response.Adapt<GetCategoryResposne>();
                }

            return default;
        }
    }
}
