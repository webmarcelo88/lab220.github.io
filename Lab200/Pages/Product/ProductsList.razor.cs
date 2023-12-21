using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Product;

public partial class ProductsList
{
    #region Injection
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] IProductService _productService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Product> Products { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        Products = await _productService.GetProductsByClientAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
        Products= Products.OrderBy(x => x.Id).ToList();
        StateHasChanged();
    }

    private void AddNewProduct() => _navigationManager.NavigateTo(Routes.CREATE_PRODUCTS);

    private void EditProduct(int productId)
    {
        _navigationManager.NavigateTo($"/products/{productId}/edit");
    }

    private async Task DeleteProduct(int productId)
    {
        var product = Products.Where(x => x.Id == productId).FirstOrDefault();

        if (product is null)
        {
            _snackbar.Add($"Produto não encontrado!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(product.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _productService.DeleteProductAsync(product);
            if (removed > 0)
            {
                _snackbar.Add($"Produto {product.Name} removido com sucesso!", MudBlazor.Severity.Success);
                Products = await _productService.GetProductsByClientAsync(_sessionState.User.ClientId ?? 32);
                Products.Remove(product);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.PRODUCTS);
            }
            else
            {
                _snackbar.Add($"Erro ao remover produto {product.Name}!", MudBlazor.Severity.Error);
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
