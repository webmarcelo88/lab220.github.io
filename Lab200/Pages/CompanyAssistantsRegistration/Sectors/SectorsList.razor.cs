using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.Sectors;

public partial class SectorsList
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] ISectorService _sectorService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Sector> Sectors { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        Sectors = await _sectorService.GetSectorByClientAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
        StateHasChanged();
        await base.OnInitializedAsync();
    }

    private void AddNewSector() => _navigationManager.NavigateTo(Routes.CREATE_SECTORS);

    private async Task DeleteSector(int sectorId)
    {
        var sector = Sectors.Where(x => x.Id == sectorId).FirstOrDefault();

        if (sector is null)
        {
            _snackbar.Add($"Centro de custo não encontrado!", Severity.Error);
            Sectors = await _sectorService.GetSectorByClientAsync(_sessionState.User.ClientId ?? 32);
            StateHasChanged();
            return;
        }

        var shouldDelete = await InvokeDeleteModalAsync(sector.Name);
        if (shouldDelete)
        {
            var removed = await _sectorService.DeleteSectorAsync(sector);
            Sectors = await _sectorService.GetSectorByClientAsync(_sessionState.User.ClientId ?? 32);
            Sectors.Remove(sector);
            StateHasChanged();
            _navigationManager.NavigateTo(Routes.SECTORS);
        }
        else
        {
            _snackbar.Add($"Erro ao remover o setor {sector.Name}!", Severity.Error);
        }

        StateHasChanged();
    }

    private void EditSector(int sectorId)
    {
        _navigationManager.NavigateTo($"/company-assistants/sectors/{sectorId}/edit");
    }

    private async Task<bool> InvokeDeleteModalAsync(string sectorName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o setor: {sectorName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover setor", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}
