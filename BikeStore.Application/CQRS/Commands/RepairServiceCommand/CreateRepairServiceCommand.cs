using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.DTO.Request.RepairServiceRequest;
using BikeStore.Domain.DTO.Response.RepairServiceResposne;
using BikeStore.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.RepairServiceCommand
{
    public record CreateRepairServiceCommand(AddNewRepairRequest Request) : ICommand<CreateRepairServiceResponse>;

    public class CreateRepairServiceCommandHandler : ICommandHandler<CreateRepairServiceCommand, CreateRepairServiceResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRepairServiceCommandHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CreateRepairServiceResponse> Handle(CreateRepairServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var RepairSerivceObj = request.Request.Adapt<RepairService>();

                var InsertedRepairService = await _unitOfWork.repairServiceRepository.AddNewRepairServiceAsync(RepairSerivceObj);

                if (InsertedRepairService is null)
                {
                    return default;
                }



                var repairIssuesList = request.Request.IssueList.Select(issue => new RepairIssues
                {
                    RepairServiceId = InsertedRepairService.ServiceId,
                    IssueFixed = false,
                    IssueDescription = issue.IssueDescription
                }).ToList();

                var InvoiceObj = new Invoice
                {
                    InvoiceDate = DateTime.Now,
                    TotalAmount = request.Request.TotalAmount,
                    ServiceId = InsertedRepairService.ServiceId,
                    IsPaid = false,
                    Type = "Repair Service"
                };

                var insertedInvoiceId = await _unitOfWork.InvoiceRepository.AddNewInvoice(InvoiceObj);

                var invoiceItemsList = request.Request.SpareParts.Select(part => new InvoiceItems
                {
                    InvoiceId = insertedInvoiceId,
                    Description = part.Description,
                    Quantity = part.Quantity,
                    UnitPrice = part.UnitPrice
                }).ToList();


                await _unitOfWork.RepairIssuesRepository.AddRepairIssues(repairIssuesList);

                await _unitOfWork.InvoiceItemsRepository.AddInvoiceItems(invoiceItemsList);


                var isSuccess = await _unitOfWork.SaveAsync();

                if (!isSuccess)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return new CreateRepairServiceResponse();
                }

                await _unitOfWork.CommitTransactionAsync();

                return InsertedRepairService;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return default;
            }

        }
    }

}
