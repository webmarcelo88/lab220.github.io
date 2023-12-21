using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Protocol;
using MudBlazor;

namespace Lab200.Pages.Configurations.Themes;

public partial class Theme
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] IThemeService _themeService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Theme> Themes { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        Themes = await _themeService.GetAllThemesAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
        StateHasChanged();
    }

    private void AddNewTheme() => _navigationManager.NavigateTo(Routes.CREATE_THEMES);

    private void EditTheme(int themeId)
    {
        _navigationManager.NavigateTo($"/configuration/themes/{themeId}/edit");
    }

    private async Task DeleteTheme(int themeId)
    {
        var theme = Themes.Where(x=>x.Id == themeId).FirstOrDefault();

        if(theme is null)
        {
            _snackbar.Add($"Tema não encontrado!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(theme.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _themeService.DeleteThemeAsync(theme);
            if (removed > 0)
            {
                _snackbar.Add($"Tema {theme.Name} removido com sucesso!", MudBlazor.Severity.Success);
                Themes = await _themeService.GetAllThemesAsync(_sessionState.User.ClientId ?? 32);
                Themes.Remove(theme);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.THEMES);
            }
            else
            {
                _snackbar.Add($"Erro ao remover tema {theme.Name}!", MudBlazor.Severity.Error);
            }
            #endregion
            StateHasChanged();
        }

    }

    private async Task<bool> InvokeDeleteModalAsync(string themeName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o tema: {themeName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover tema", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}
