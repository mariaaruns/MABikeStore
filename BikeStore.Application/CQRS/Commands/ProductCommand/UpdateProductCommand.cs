using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO.Request.ProductRequest;
using BikeStore.Domain.DTO.Response.ProductResponse;
using BikeStore.Domain.Models;
using Mapster;

namespace BikeStore.Application.CQRS.Commands.ProductCommand
{

    public record UpdateProductCommand(UpdateProductRequest Command,string ImagePath) : ICommand<GetProductResponse>;
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, GetProductResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork,IFileService fileService)
        {
            this._unitOfWork = unitOfWork;
            this._fileService = fileService;
        }
        public async Task<GetProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var Product = await  _unitOfWork.ProductRepository.GetByIdAsync(x => x.ProductId == request.Command.ProductId);
            if (Product is null) 
            {
                return null!;
            }

            if (request.Command.ImageFile != null) 
            {

                if (!string.Equals(request.Command.ImageFile.FileName, Product.Image,StringComparison.OrdinalIgnoreCase)) 
                {
                    if (!string.IsNullOrEmpty(Product.Image))
                    {
                        var oldImageFilePath= Path.Combine(request.ImagePath, Product.Image);
                        _fileService.DeleteFile(oldImageFilePath);
                    }
                    var newImageName = await _fileService.SaveFileAsync(request.Command.ImageFile,request.ImagePath);
                    request.Command.Image = newImageName;
                }
            }

            var ProductEntity = request.Command.Adapt<Product>();
            var result =await _unitOfWork.ProductRepository.UpdateProductAsync(ProductEntity);
            bool isSuccess = await _unitOfWork.SaveAsync();
            if (isSuccess) 
            {
                var MapResultToResponse = result.Adapt<GetProductResponse>();
                return MapResultToResponse;
            }
            return default!;
        }
    }

}
