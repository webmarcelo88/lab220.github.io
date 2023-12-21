using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.Function;

public partial class EditFunction
{
    #region Injections
    [Inject] IFunctionService _functionService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    [Parameter] public int FunctionId { get; set; }

    public Entities.Function Function { get; set; } = null!;
    public List<CostCenter> CostCenters { get; set; } = null!;

    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        CostCenters = await _costCenterService.GetCostCentersByClientAsync(_sessionState.User.ClientId ?? 32);
        Function = await _functionService.GetFunctionByIdAsync(FunctionId);
        if (CostCenters == null || !CostCenters.Any())
        {
            _snackbar.Add($"É necessário ter ao menos um centro de custo cadastrado!", Severity.Error);
            _navigationManager.NavigateTo(Routes.FUNCTIONS);
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

        var isUpdated = await _functionService.UpdateFunctionAsync(Function);
        if (isUpdated != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.FUNCTIONS);

        _isProcessing = false;
    }
}
