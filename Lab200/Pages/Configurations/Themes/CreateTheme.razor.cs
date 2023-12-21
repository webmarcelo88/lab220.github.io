using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.Configurations.Themes;

public partial class CreateTheme
{
    #region Injections
    [Inject] IThemeService _themeService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Entities.Theme Theme { get; set; } = new();

    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Theme.ClientId = _sessionState.User.ClientId ?? 32;
        Theme.IsDeleted = false;
        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _themeService.CreateThemeAsync(Theme);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.THEMES);

        _isProcessing = false;
    }
}
