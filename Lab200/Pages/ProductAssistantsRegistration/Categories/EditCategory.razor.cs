using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.ProductAssistantsRegistration.Categories;

public partial class EditCategory
{
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] ICategoryService _categoryService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    [Parameter] public int CategoryId { get; set; }

    public bool IsLoading { get; set; } = true;
    public Category? Category { get; set; } = null!;


    protected async override Task OnInitializedAsync()
    {
        if(CategoryId == 0)
        {
            _snackbar.Add("Categoria não informada ou inválida!", Severity.Info);
            _navigationManager.NavigateTo(Routes.CATEGORIES);
            return;
        }

        Category = await _categoryService.GetCategoryByIdAsync(CategoryId);

        if(Category == null)
        {
            _snackbar.Add("Categoria não existe!", Severity.Error);
            _navigationManager.NavigateTo(Routes.CATEGORIES);
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

        var successfullySaved = await _categoryService.UpdateCategoryAsync(Category!);
        if (successfullySaved == 0)
        {
            _snackbar.Add($"Não foi possível atualizar os dados da categoria: {Category!.Name}!", Severity.Error);
            return;
        }
        else
        {
            _progressPercent = 75;
            StateHasChanged();

            _snackbar.Add($"Dados atualizados com sucesso!", Severity.Success);

            _progressPercent = 100;
            StateHasChanged();

            _navigationManager.NavigateTo(Routes.CATEGORIES);
        }

    }
}
