using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.CostCenters;

public partial class EditCostCenter
{
    #region Injections
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    [Parameter] public int CostCenterId { get; set; }

    public Entities.CostCenter? CostCenter { get; set; }


    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        CostCenter = await _costCenterService.GetCostCentersById(CostCenterId);
        if (CostCenter == null)
        {
            _snackbar.Add($"Centro de custo não foi encontrado!", Severity.Error);
            _navigationManager.NavigateTo(Routes.COST_CENTERS);
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

        var isUpdated = await _costCenterService.UpdateCostCenterAsync(CostCenter);
        if (isUpdated != 0)
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