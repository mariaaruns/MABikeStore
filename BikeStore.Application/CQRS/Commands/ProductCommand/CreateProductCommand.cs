﻿using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Application.CQRS.Queries.ProductQueries;
using BikeStore.Domain.Contracts;
using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO.Request.ProductRequest;
using BikeStore.Domain.DTO.Response.ProductResponse;
using BikeStore.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.ProductCommand
{
    public record CreateProductCommand(CreateProductRequest Command,string ImagePath) : ICommand<GetProductResponse>;

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, GetProductResponse>
    {
        private readonly IUnitOfWork  _unitOfWork;
        private readonly IFileService _fileService;

        public CreateProductCommandHandler(IUnitOfWork unitOfwork,IFileService fileService)
        {
            this._unitOfWork = unitOfwork;
            this._fileService = fileService;
        }
        public async Task<GetProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Command.ImageFile != null)
            {
                var newImageName = await _fileService.SaveFileAsync(request.Command.ImageFile, request.ImagePath);
                request.Command.Image = newImageName;
            }

            var ProductEntity = request.Command.Adapt<Product>();
            var result = await _unitOfWork.ProductRepository.CreateAsync(ProductEntity);
            bool isSuccess = await _unitOfWork.SaveAsync();
            if (isSuccess)
            {
                return result.Adapt<GetProductResponse>(); ;
            }
            return default!;
        }
    }
}
