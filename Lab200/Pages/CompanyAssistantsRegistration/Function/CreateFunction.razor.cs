using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.CompanyAssistantsRegistration.Function;

public partial class CreateFunction
{
    #region Injections
    [Inject] IFunctionService _functionService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Entities.Function Function { get; set; } = new();
    public List<CostCenter> CostCenters { get; set; } = null!;
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        CostCenters = await _costCenterService.GetCostCentersByClientAsync(_sessionState.User.ClientId ?? 32);
        await base.OnInitializedAsync();
    }
    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Function.ClientId = _sessionState.User.ClientId ?? 32;
        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _functionService.AddNewFunctionAsync(Function);
        if (isRegister != 0)
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
