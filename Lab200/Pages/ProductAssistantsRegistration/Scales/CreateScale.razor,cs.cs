using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.ProductAssistantsRegistration.Scales;
public partial class CreateScale
{
    #region Injections 
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IScaleService _scaleService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Scale Scale { get; set; } = new();

    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Scale.ClientId = _sessionState.User.ClientId ?? 32;
        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _scaleService.CreateScaleAsync(Scale);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.SCALES);

        _isProcessing = false;
    }
}