using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.CompanyAssistantsRegistration.CostCenters;

public partial class CreateCostCenter
{
    #region Injections
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Entities.CostCenter CostCenter { get; set; } = new();
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        CostCenter.ClientId = _sessionState.User.ClientId ?? 32;
        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _costCenterService.CreateCostCenterAsync(CostCenter);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.COST_CENTERS);

        _isProcessing = false;
    }
}