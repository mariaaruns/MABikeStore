using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.BrandCommand
{

    public record DeleteBrandCommand(int id):ICommand<bool>;
    public class DeleteBrandCommandHandler : ICommandHandler<DeleteBrandCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(x => x.BrandId == request.id);
            if (brand is null) {

                return false;
            }

            await _unitOfWork.BrandRepository.DeleteAsync(brand);

            return await _unitOfWork.SaveAsync();
            
        }
    }
}
