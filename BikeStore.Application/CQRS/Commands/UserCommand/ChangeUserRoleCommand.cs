using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts.IRepository;
using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.Domain.DTO.Response.UserResponse;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.CQRS.Commands.UserCommand
{
    public record ChangeUserRoleCommand(ChangeUserRoleRequest Req):ICommand<ChangeUserRoleResponse>;

    public class ChangeUserRoleCommandHandler(IAuthenticationService authService) : ICommandHandler<ChangeUserRoleCommand, ChangeUserRoleResponse>
    {
        private readonly IAuthenticationService _authService = authService;

        public Task<ChangeUserRoleResponse> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = _authService.ChangeUserRoleAsync(request.Req);
            return result;
        }
    }


    public class ChangeUserRoleValidator : AbstractValidator<ChangeUserRoleCommand> {

        public ChangeUserRoleValidator()
        {
            RuleFor(x => x.Req.UserId).NotEmpty();
            RuleFor(x => x.Req.OldUserRole).NotEmpty();
            RuleFor(x => x.Req.NewUserRole).NotEmpty();
        }
    }
}
