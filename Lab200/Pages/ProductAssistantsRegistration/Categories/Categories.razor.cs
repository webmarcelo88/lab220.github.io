using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.ProductAssistantsRegistration.Categories;

public partial class Categories
{
    #region Injection
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] ICategoryService _categoryService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Category> CategoriesList { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        CategoriesList = await _categoryService.GetAllCategoriesAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
    }

    private void AddNewCategory() => _navigationManager.NavigateTo(Routes.CREATE_CATEGORY);

    private void EditCategory(int categoryId)
    {
        _navigationManager.NavigateTo($"/product-assistants/categories/{categoryId}/edit");
    }

    private async Task DeleteCategory(int categoryId)
    {
        var category = CategoriesList.Where(x => x.Id == categoryId).FirstOrDefault();

        if (category is null)
        {
            _snackbar.Add($"Categoria não encontrada!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(category.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _categoryService.DeleteCategoryAsync(category);
            if (removed > 0)
            {
                _snackbar.Add($"Categoria {category.Name} removida com sucesso!", MudBlazor.Severity.Success);
                CategoriesList = await _categoryService.GetAllCategoriesAsync(_sessionState.User.ClientId ?? 32);
                CategoriesList.Remove(category);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.CATEGORIES);
            }
            else
            {
                _snackbar.Add($"Erro ao remover categoria {category.Name}!", MudBlazor.Severity.Error);
            }
            #endregion
            StateHasChanged();
        }
        return;
    }

    private async Task<bool> InvokeDeleteModalAsync(string categoryName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover a categoria: {categoryName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover categoria", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}