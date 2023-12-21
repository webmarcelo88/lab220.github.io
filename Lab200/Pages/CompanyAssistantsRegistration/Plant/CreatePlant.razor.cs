using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.CompanyAssistantsRegistration.Plant;

public partial class CreatePlant
{
    #region Injections
    [Inject] IPlantService _plantsService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Entities.Plant Plant { get; set; } = new();
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Plant.ClientId = _sessionState.User.ClientId ?? 32;
        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _plantsService.CreatePlantAsync(Plant);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();
        
        _navigationManager.NavigateTo(Routes.PLANTS);

        _isProcessing = false;
    }
}
