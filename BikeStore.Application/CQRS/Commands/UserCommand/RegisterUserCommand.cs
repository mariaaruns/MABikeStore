using BikeStore.Application.Abstraction.Messaging;
using BikeStore.Domain.Contracts;
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

    public record RegisterUserCommand(UserRegisterRequest request):ICommand<RegisterResponse>;

    class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, RegisterResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork,IAuthenticationService authenticationService)
        {
            this._unitOfWork = unitOfWork;
            this._authenticationService = authenticationService;
        }

        public async Task<RegisterResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
         var response=await _authenticationService.RegisterAsync(command.request);
         return response;
        }
    }


    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand> {

        public RegisterUserValidator()
        {
            RuleFor(x => x.request.Email).EmailAddress();
            RuleFor(x => x.request.Password).MaximumLength(16).MinimumLength(6);
        }

    }

}
