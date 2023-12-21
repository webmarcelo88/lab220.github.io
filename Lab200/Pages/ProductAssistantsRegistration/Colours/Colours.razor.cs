using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.ProductAssistantsRegistration.Colours;

public partial class Colours
{
    #region Injection
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] IColoursService _coloursService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Colours> ColoursList { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        ColoursList = await _coloursService.GetAllColoursAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
        StateHasChanged();
    }

    private void AddNewColour() => _navigationManager.NavigateTo(Routes.CREATE_COLOUR);

    private void EditColour(int colourId)
    {
        _navigationManager.NavigateTo($"/product-assistants/colours/{colourId}/edit");
    }

    private async Task DeleteColour(int colourId)
    {
        var colour = ColoursList.Where(x => x.Id == colourId).FirstOrDefault();

        if (colour is null)
        {
            _snackbar.Add($"Cor não encontrada!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(colour.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _coloursService.DeleteColourAsync(colour);
            if (removed > 0)
            {
                _snackbar.Add($"Cor {colour.Name} removida com sucesso!", MudBlazor.Severity.Success);
                ColoursList = await _coloursService.GetAllColoursAsync(_sessionState.User.ClientId ?? 32);
                ColoursList.Remove(colour);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.COLOURS);
            }
            else
            {
                _snackbar.Add($"Erro ao remover cor {colour.Name}!", MudBlazor.Severity.Error);
            }
            #endregion
            StateHasChanged();
        }
        return;
    }

    private async Task<bool> InvokeDeleteModalAsync(string colourName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover a cor: {colourName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover Cor", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}
