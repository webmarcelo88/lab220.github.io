
using Lab200.Data;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.ProductAssistantsRegistration.Scales;
public partial class EditScale
{
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] IScaleService _scaleService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    [Parameter] public int ScaleId { get; set; }

    public bool IsLoading { get; set; } = true;
    public Entities.Scale? Scale { get; set; } = null!;

    protected async override Task OnInitializedAsync()
    {
        if (ScaleId == 0)
        {
            _snackbar.Add("Tamanho não informado ou inválido!", Severity.Info);
            _navigationManager.NavigateTo(Routes.SCALES);
        }

        Scale = await _scaleService.GetScaleByIdAsync(ScaleId);

        if (Scale == null)
        {
            _snackbar.Add("Tamanho não existe!", Severity.Error);
            _navigationManager.NavigateTo(Routes.SCALES);
            return;
        }

        IsLoading = false;

        await base.OnInitializedAsync();
    }

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();

        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var successfullySaved = await _scaleService.UpdateScaleAsync(Scale!);
        if (successfullySaved == 0)
        {
            _snackbar.Add($"Não foi possível atualizar os dados do tamanho: {Scale!.Name}!", Severity.Error);
            return;
        }
        else
        {
            _progressPercent = 75;
            StateHasChanged();

            _snackbar.Add($"Dados atualizados com sucesso!", Severity.Success);

            _progressPercent = 100;
            StateHasChanged();

            _navigationManager.NavigateTo(Routes.SCALES);
        }
    }
}