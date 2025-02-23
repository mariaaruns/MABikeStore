using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Application.CQRS.Commands.BrandCommand;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.UserCommand
{
    public record AddRoleClaimCommand(AddRoleClaimRequest Request):ICommand<AddRoleClaimResponse>;
    public class AddRoleClaimCommandHandler : ICommandHandler<AddRoleClaimCommand, AddRoleClaimResponse>
    {
        private readonly IAuthenticationService _authService;

        public AddRoleClaimCommandHandler(IAuthenticationService authenticationService)
        {
            this._authService = authenticationService;
        }
        public Task<AddRoleClaimResponse> Handle(AddRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var result = _authService.AddRoleClaimAsync(request.Request);
            return result;
        }
    }


    public record EditRoleClaimCommand(EditRoleClaimRequest Request) : ICommand<EditRoleClaimResponse>;
    public class EditRoleClaimCommandHandler : ICommandHandler<EditRoleClaimCommand, EditRoleClaimResponse>
    {
        private readonly IAuthenticationService _authService;

        public EditRoleClaimCommandHandler(IAuthenticationService authenticationService)
        {
            this._authService = authenticationService;
        }
        public Task<EditRoleClaimResponse> Handle(EditRoleClaimCommand request, CancellationToken cancellationToken)
        {

            var result = _authService.EditRoleClaimAsync(request.Request);
            return result;
        }
    }


    public record DeleteRoleClaimCommand(AddRoleClaimRequest Request) : ICommand<AddRoleClaimResponse>;
    public class DeleteRoleClaimCommandHandler : ICommandHandler<DeleteRoleClaimCommand, AddRoleClaimResponse>
    {
        private readonly IAuthenticationService _authService;

        public DeleteRoleClaimCommandHandler(IAuthenticationService authenticationService)
        {
            this._authService = authenticationService;
        }
        public Task<AddRoleClaimResponse> Handle(DeleteRoleClaimCommand request, CancellationToken cancellationToken)
        {
            var result = _authService.DeleteRoleClaimAsync(request.Request);
            return result;
        }
    }


    public class AddRoleValidator : AbstractValidator<AddRoleClaimCommand>
    {
        public AddRoleValidator()
        {
            RuleFor(x => x.Request.RoleName).NotEmpty();
            RuleFor(x => x.Request.ClaimType).NotEmpty();
            RuleFor(x => x.Request.ClaimValue).NotEmpty();
            //RuleFor(x => x.request.Logo).NotEmpty();
        }

    }


    public class EditRoleValidator : AbstractValidator<EditRoleClaimCommand>
    {
        public EditRoleValidator()
        {
            RuleFor(x => x.Request.RoleName).NotEmpty();
            RuleFor(x => x.Request.ClaimType).NotEmpty();
            RuleFor(x => x.Request.ClaimValue).NotEmpty();
            RuleFor(x => x.Request.OldClaimValue).NotEmpty();
            RuleFor(x => x.Request.OldClaimValue).NotEmpty();
        }

    }

    public class DeleteRoleValidator : AbstractValidator<DeleteRoleClaimCommand>
    {
        public DeleteRoleValidator()
        {
            RuleFor(x => x.Request.RoleName).NotEmpty();
            RuleFor(x => x.Request.ClaimType).NotEmpty();
            RuleFor(x => x.Request.ClaimValue).NotEmpty();
        }
    }


}
