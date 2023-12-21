using Lab200.Data;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Configurations.Themes;

public partial class EditTheme
{
    #region Injections
    [Inject] IThemeService _themeService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    [Parameter] public int ThemeId { get; set; }

    public Entities.Theme Theme { get; set; } = null!;

    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        Theme = await _themeService.GetThemeByIdAsync(ThemeId);
        if (Theme == null)
        {
            _snackbar.Add($"Tema não foi encontrado!", Severity.Error);
            _navigationManager.NavigateTo(Routes.THEMES);
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

        var isUpdated = await _themeService.UpdateThemeAsync(Theme);
        if (isUpdated != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.THEMES);

        _isProcessing = false;
    }
}
