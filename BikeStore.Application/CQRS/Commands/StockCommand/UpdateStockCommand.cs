using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.Stock;
using BikeStore.Domain.DTO.Response.Stock;
using BikeStore.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.StockCommand
{
    public record UpdateStockCommand(UpsertStockRequest req):ICommand<GetStockResponse>;
    public class UpdateStockCommandHandler : ICommandHandler<UpdateStockCommand, GetStockResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStockCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<GetStockResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var mapReqToEntity = request.req.Adapt<Stock>();
            var result = await _unitOfWork.stockRepository.UpdateQtyAsync(mapReqToEntity);
           var mapEntityToRes=result.Adapt<GetStockResponse>();
            return mapEntityToRes;
        }
    }
}
