using BikeStore.Domain.DTO.Request.UserRequest;
using BikeStore.UI.Contracts.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BikeStore.UI.Pages.User
{
    public class LoginBase:ComponentBase
    {
        [Inject]
        protected IAuthenticationService _authenticationService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected LoginRequest LoginModel = new LoginRequest();
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }

        protected MudForm _form;

        protected async Task Submit()
        {
            _form.Validate();
            if (_form.IsValid)
            {
               await LoginAsync();
            }
        }

        protected async Task LoginAsync()
        {
            var result = await _authenticationService.Login(LoginModel);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Error = result.Message;
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }

        }
    }
}
