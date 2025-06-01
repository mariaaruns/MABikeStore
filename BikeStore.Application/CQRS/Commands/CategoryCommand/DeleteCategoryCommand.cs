using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.CategoryCommand
{
    public record DeleteCategoryCommand(int id) : ICommand<bool>;
    public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var Category = await _unitOfWork.CategoryRepository.GetByIdAsync(x => x.CategoryId == request.id);

            if (Category is null)
            {
                throw new FluentValidation.ValidationException("Invalid category");
            }
            await _unitOfWork.CategoryRepository.InactiveAsync(Category);
            return await _unitOfWork.SaveAsync();
        }
    }
}


