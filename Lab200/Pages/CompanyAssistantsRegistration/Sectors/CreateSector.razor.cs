using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.CompanyAssistantsRegistration.Sectors;

public partial class CreateSector
{
    #region Injections
    [Inject] ISectorService _sectorService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Entities.Sector Sector { get; set; } = new();
    public List<Entities.CostCenter> CostCenters { get; set; } = null!;

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
        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _sectorService.CreateSectorAsync(Sector);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.SECTORS);

        _isProcessing = false;
    }
}
