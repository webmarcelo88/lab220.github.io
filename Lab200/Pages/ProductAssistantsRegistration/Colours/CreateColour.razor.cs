using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.ProductAssistantsRegistration.Colours;

public partial class CreateColour
{
    #region Injections
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IColoursService _coloursService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Entities.Colours Colour { get; set; } = new();

    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Colour.ClientId = _sessionState.User.ClientId ?? 32;

        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _coloursService.CreateColourAsync(Colour);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.COLOURS);

        _isProcessing = false;

        return;
    }
}
