using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.ProductAssistantsRegistration.Scales;
public partial class Scales
{
    #region Injection
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] IScaleService _scaleService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Scale> ScalesList { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        ScalesList = await _scaleService.GetAllScalesAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
        await base.OnInitializedAsync();
    }

    private void AddNewScale() => _navigationManager.NavigateTo(Routes.CREATE_SCALES);

    private void EditScale(int scaleId)
    {
        _navigationManager.NavigateTo($"/product-assistants/scales/{scaleId}/edit");
    }


    private async Task DeleteScale(int scaleId)
    {
        var scale = ScalesList.Where(x => x.Id == scaleId).FirstOrDefault();

        if (scale is null)
        {
            _snackbar.Add($"Tamanho não encontrado!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(scale.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _scaleService.DeleteScaleAsync(scale);
            if (removed > 0)
            {
                _snackbar.Add($"Tamanho {scale.Name} removido com sucesso!", MudBlazor.Severity.Success);
                ScalesList = await _scaleService.GetAllScalesAsync(_sessionState.User.ClientId ?? 32);
                ScalesList.Remove(scale);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.SCALES);
            }
            else
            {
                _snackbar.Add($"Erro ao remover o tamanho {scale.Name}!", MudBlazor.Severity.Error);
            }
            #endregion
            StateHasChanged();
        }
        return;
    }
    private async Task<bool> InvokeDeleteModalAsync(string scaleName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o tamanho: {scaleName}?" },
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