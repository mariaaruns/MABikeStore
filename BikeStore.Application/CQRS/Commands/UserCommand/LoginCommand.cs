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
using System.Windows.Input;

namespace BikeStore.Application.CQRS.Commands.UserCommand
{

    public record LoginCommand(LoginRequest command):ICommand<LoginResponse>;
    public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
    {
        private readonly IAuthenticationService _authenticationService;

       public LoginCommandHandler(IAuthenticationService authenticationService) {
            this._authenticationService = authenticationService;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var Response =await _authenticationService.LoginAsync(request.command);
            return Response;
        }
    }


    public class LoginValidator : AbstractValidator<LoginCommand> {

        public LoginValidator()
        {
            RuleFor(x => x.command.UserName).NotEmpty();
            RuleFor(x => x.command.Password).NotEmpty();
        }
    }

}
