using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.ProductCommand
{
    public record DeleteProductCommand(int Id):ICommand<bool>;
    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0) 
            {
                await Task.CompletedTask;
                return false;
            }
            var Product = await _unitOfWork.ProductRepository.GetByIdAsync(x => x.ProductId == request.Id);
          
            if (Product !=null) 
            {
                await _unitOfWork.ProductRepository.DeleteAsync(Product);
            }
            var result =await _unitOfWork.SaveAsync();
            return result;
        }
    }



}
