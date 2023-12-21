using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Company.WebUser;

public partial class EditWebUser
{
    #region Injections
    [Inject] IUserService _userService { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    [Parameter] public int UserId { get; set; }
    
    public User? User { get; set; }

    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        User = await _userService.GetUserByIdAsync(UserId);
        if(User == null)
        {
            _snackbar.Add($"Usuário não foi encontrado!", Severity.Error);
            _navigationManager.NavigateTo(Routes.COMPANY_WEB_USER);
            return;
        }

        _isLoading = false;
    }

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var isUpdated = await _userService.UpdateUserAsync(User);
        if (isUpdated != 0)
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
