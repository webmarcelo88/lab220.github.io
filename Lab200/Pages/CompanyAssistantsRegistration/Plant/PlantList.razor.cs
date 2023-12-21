using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.Plant;

public partial class PlantList
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] IPlantService _plantsService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Plant> Plants { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        Plants = await _plantsService.GetAllPlantsByClientAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
        StateHasChanged();
        await base.OnInitializedAsync();
    }

    private void AddNewPlant() => _navigationManager.NavigateTo(Routes.CREATE_PLANTS);

    private async Task DeletePlant(int plantId)
    {
        var plant = Plants.Where(x => x.Id == plantId).FirstOrDefault();

        if (plant is null)
        {
            _snackbar.Add($"Planta não encontrada!", Severity.Error);
            Plants = await _plantsService.GetAllPlantsByClientAsync(_sessionState.User.ClientId ?? 32);
            StateHasChanged();
            return;
        }

        var shouldDelete = await InvokeDeleteModalAsync(plant.Name);
        if (shouldDelete)
        {
            var removed = await _plantsService.DeletePlantAsync(plant);
            Plants = await _plantsService.GetAllPlantsByClientAsync(_sessionState.User.ClientId ?? 32);
            Plants.Remove(plant);
            StateHasChanged();
            _navigationManager.NavigateTo(Routes.PLANTS);
        }
        else
        {
            _snackbar.Add($"Erro ao remover a planta {plant.Name}!", Severity.Error);
        }

        StateHasChanged();
    }

    private void EditPlant(int plantId)
    {
        _navigationManager.NavigateTo($"/company-assistants/plants/{plantId}/edit");
    }

    private async Task<bool> InvokeDeleteModalAsync(string costCenterName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover a função: {costCenterName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover função", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}
