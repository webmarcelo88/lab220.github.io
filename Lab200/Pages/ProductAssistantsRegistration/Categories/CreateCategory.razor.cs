using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.ProductAssistantsRegistration.Categories;

public partial class CreateCategory
{
    #region Injections
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ICategoryService _categoryService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    public Category Category { get; set; } = new();

    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Category.ClientId = _sessionState.User.ClientId ?? 32;
        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _categoryService.CreateCategoryAsync(Category);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.CATEGORIES);

        _isProcessing = false;
    }
}
