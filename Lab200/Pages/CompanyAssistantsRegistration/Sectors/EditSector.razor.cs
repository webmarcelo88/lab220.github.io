using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.Sectors;

public partial class EditSector
{
    #region Injections
    [Inject] ISectorService _sectorService { get; set; } = null!;
    [Inject] ICostCenterService _costCenterService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    [Parameter] public int SectorId { get; set; }

    public Entities.Sector Sector { get; set; } = null!;

    public List<Entities.CostCenter> CostCenters { get; set; } = null!;

    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        Sector = await _sectorService.GetSectorByIdAsync(SectorId);
        CostCenters = await _costCenterService.GetCostCentersByClientAsync(_sessionState.User.ClientId ?? 32);
        if (Sector == null)
        {
            _snackbar.Add($"Setor não foi encontrado!", Severity.Error);
            _navigationManager.NavigateTo(Routes.SECTORS);
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

        var isUpdated = await _sectorService.UpdateSectorAsync(Sector);
        if (isUpdated != 0)
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
