using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.StoreCommand
{

    public record DeleteStoreCommand(int id):ICommand<bool>;
    public class DeleteStoreCommandHandler : ICommandHandler<DeleteStoreCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStoreCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            var getStore = await _unitOfWork.StoreRepository.GetByIdAsync(x => x.StoreId == request.id);
            if (getStore is null)
            {
                return false;
            }
            else 
            {
                await _unitOfWork.StoreRepository.DeleteAsync(getStore);
                var result = await _unitOfWork.SaveAsync();
                return result ;
            }
        }
    }
}
