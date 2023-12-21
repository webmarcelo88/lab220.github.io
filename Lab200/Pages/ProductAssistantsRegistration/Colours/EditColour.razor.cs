using Lab200.Data;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.ProductAssistantsRegistration.Colours;
public partial class EditColour
{
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] IColoursService _colourService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    [Parameter] public int ColourId { get; set; }

    public bool IsLoading { get; set; } = true;
    public Entities.Colours? Colour { get; set; } = null!;

    protected async override Task OnInitializedAsync()
    {
        if (ColourId == 0)
        {
            _snackbar.Add("Cor não informada ou inválida!", Severity.Info);
            _navigationManager.NavigateTo(Routes.COLOURS);
        }

        Colour = await _colourService.GetColourByIdAsync(ColourId);

        if (Colour == null)
        {
            _snackbar.Add("Cor não existe!", Severity.Error);
            _navigationManager.NavigateTo(Routes.COLOURS);
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

        var successfullySaved = await _colourService.UpdateColourAsync(Colour!);
        if (successfullySaved == 0)
        {
            _snackbar.Add($"Não foi possível atualizar os dados da categoria: {Colour!.Name}!", Severity.Error);
            return;
        }
        else
        {
            _progressPercent = 75;
            StateHasChanged();

            _snackbar.Add($"Dados atualizados com sucesso!", Severity.Success);

            _progressPercent = 100;
            StateHasChanged();

            _navigationManager.NavigateTo(Routes.COLOURS);
        }
    }
}