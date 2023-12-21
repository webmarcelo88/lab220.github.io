using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.Product;

public partial class EditProduct
{
    #region Injections
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IProductService _productService { get; set; } = null!;
    [Inject] ICategoryService _categoryService { get; set; } = null!;
    [Inject] IColoursService _colourService { get; set; } = null!;
    [Inject] IScaleService _scaleService { get; set; } = null!;
    [Inject] IGridService _gridService { get; set; } = null!;
    [Inject] IPlantService _plantsService { get; set; } = null!;
    [Inject] IProductTypeService _productTypeService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    [Parameter] public int ProductId { get; set; }

    public Entities.Product Product { get; set; } = new();
    public List<Grid> Grids { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public List<Colours> Colours { get; set; } = new();
    public List<Scale> Scales { get; set; } = new();
    public List<Plant> Plants { get; set; } = new();
    public List<ProductType> ProductTypes { get; set; } = new();

    private bool IsTableLoading { get; set; } = true;
    private bool _isProcessing = false;
    private int _progressPercent = 0;


    protected async override Task OnInitializedAsync()
    {
        Product = await _productService.GetProductByIdAsync(ProductId);
        Grids = await _gridService.GetGridsByClientAndProductIdAsync(_sessionState.User.ClientId ?? 32, ProductId);
        Categories = await _categoryService.GetAllCategoriesAsync(_sessionState.User.ClientId ?? 32);
        Colours = await _colourService.GetAllColoursAsync(_sessionState.User.ClientId ?? 32);
        Scales = await _scaleService.GetAllScalesAsync(_sessionState.User.ClientId ?? 32);
        Plants = await _plantsService.GetAllPlantsByClientAsync(_sessionState.User.ClientId ?? 32);
        ProductTypes = await _productTypeService.GetProductTypesByClientIdAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;

        await base.OnInitializedAsync();
    }
    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        Product.IsDeleted = false;
        _isProcessing = true;
        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _productService.UpdateProductAsync(Product);
        if (isRegister != 0)
        {
            _progressPercent = 75;
            await SaveGridsAsync();
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.PRODUCTS);

        _isProcessing = false;
    }

    private async Task SaveGridsAsync()
    {
        foreach (var grid in Grids)
        {
            await _gridService.UpdateGridAsync(grid);
        }
    }

    private async Task HandleDeleteButtonClick(Entities.Grid grid)
    {
        await _gridService.DeleteGridAsync(grid);
    }

}
