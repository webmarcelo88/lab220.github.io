using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.Plant;

public partial class EditPlant
{
    #region Injections
    [Inject] IPlantService _plantService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    [Parameter] public int PlantId { get; set; }

    public Entities.Plant Plant { get; set; }

    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        Plant = await _plantService.GetPlantByIdAsync(PlantId);
        if (Plant == null)
        {
            _snackbar.Add($"Planta não foi encontrada!", Severity.Error);
            _navigationManager.NavigateTo(Routes.PLANTS);
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

        var isUpdated = await _plantService.UpdatePlantAsync(Plant);
        if (isUpdated != 0)
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
