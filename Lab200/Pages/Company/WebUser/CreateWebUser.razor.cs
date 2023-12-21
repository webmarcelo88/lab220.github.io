using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.Company.WebUser;

public partial class CreateWebUser
{
    #region Injections
    [Inject] IUserService _userService { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    public User User { get; set; } = new();

    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        User.ClientId = _sessionState.User.ClientId ?? 32;
        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _userService.CreateUserAsync(User);
        if(isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.COMPANY_WEB_USER);

        _isProcessing = false;
    }


}
