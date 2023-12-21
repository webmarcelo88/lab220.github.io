using Lab200.Entities;
using Lab200.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using MudBlazor;
using System.Globalization;
using System.Reflection;
using static MudBlazor.CategoryTypes;
using static System.Net.Mime.MediaTypeNames;

namespace Lab200.Components.Products;

public partial class ProductForm
{
    private bool _readOnly = false;
    private bool _isCellEditMode = true;
    private List<string> _events = new();
    private bool _editTriggerRowClick;


    MudForm form;
    public bool _isLoading = false;
    public bool _isProcessing = false;
    public bool success;
    public string[] errors = { };
    public CultureInfo _br = CultureInfo.GetCultureInfo("pt-BR");

    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    [Inject] ISnackbar _snackBar { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] IWebHostEnvironment _webHostEnvironment { get; set; } = null!;

    #region Parameters
    [Parameter]
    public EventCallback OnValidSubmitAsync { get; set; }
    [Parameter]
    public EventCallback<Entities.Grid> OnDeleteButtonClickAsync { get; set; }


    [Parameter]
    public Product Product { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;

    [Parameter]
    public List<Entities.Grid> Grids { get; set; } = null!;

    [Parameter]
    public List<Category> Categories { get; set; } = new();

    [Parameter]
    public List<Plant> Plants { get; set; } = new();

    [Parameter]
    public List<ProductType> Types { get; set; } = new();

    [Parameter]
    public List<Colours> Colours { get; set; } = new();

    [Parameter]
    public List<Scale> Scales { get; set; } = new();
    #endregion

    string imageData = null!;
    private string category { get; set; }
    private string plant { get; set; }
    private string type { get; set; }

    IBrowserFile picture1 = null!;
    private string image1 { get; set; }
    private string image1Data { get; set; }

    IBrowserFile picture2 = null!;
    private string image2 { get; set; }
    private string image2Data { get; set; }

    IBrowserFile picture3 = null!;
    private string image3 { get; set; }
    private string image3Data { get; set; }

    IBrowserFile picture4 = null!;
    private string image4 { get; set; }
    private string image4Data { get; set; }

    IBrowserFile mainPicture = null!;
    private string mainImageData { get; set; }
    private string mainImage { get; set; }

    IBrowserFile detailPicture = null!;
    private string detailImageData { get; set; }
    private string detailImage { get; set; }

    protected async override Task OnInitializedAsync()
    {
        Product ??= new Product();
        Grids ??= new List<Entities.Grid>();

        if(Product.Id != (int)default)
        {
            category = Product.CategoryId.ToString();
            plant = Product.PlantId!.ToString();
            type = Product.ProductTypeId.ToString();
            GetImages();
        }

        string wwwrootPath = _webHostEnvironment.WebRootPath;
        imageData = Path.Combine(wwwrootPath, "images\\produtos");

        await base.OnInitializedAsync();
    }

    private void GetImages()
    {
        detailImageData = $"images\\produtos\\{Product.ImageDetail}";
        image1Data = $"images\\produtos\\{Product.FirstImage}";
        image2Data = $"images\\produtos\\{Product.SecondImage}";
        image3Data = $"images\\produtos\\{Product.ThirdImage}";
        image4Data = $"images\\produtos\\{Product.FourthImage}";
        mainImageData = $"images\\produtos\\{Product.FifthImage}";
    }

    private void AddNewGrid()
    {
        Grids ??= new();

        Grids.Add(new());
        StateHasChanged();
    }

    private void SetCategory()
    {
        Product.CategoryId = int.Parse(category);
    }

    private void SetPlant()
    {
        Product.PlantId = int.Parse(plant);
    }

    private void SetType()
    {
        Product.ProductTypeId = int.Parse(type);
    }

    private async Task OnPictureSelectionAsync(InputFileChangeEventArgs e, string property)
    {
        foreach (var image in e.GetMultipleFiles(int.MaxValue))
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string imagesPath = Path.Combine(wwwrootPath, $"images\\produtos\\{FileName(property)}");

            await using FileStream fs = new(imagesPath, FileMode.Create);
            await image.OpenReadStream().CopyToAsync(fs);

            SavePicture($"images\\produtos\\{FileName(property)}", property);
            StateHasChanged();
        }
    }

    private string FileName(string property)
    {
        string fileName = $"";

        switch (property)
        {
            case "detailImage":
                fileName = $"{Product.ClientId}_{Product.Code}_0.jpeg";
                detailImage = fileName;
                Product.ImageDetail = fileName;
                break;
            case "mainImage":
                fileName = $"{Product.ClientId}_{Product.Code}_5.jpeg";
                mainImage = fileName;
                Product.FifthImage = fileName;
                break;
            case "image1":
                fileName = $"{Product.ClientId}_{Product.Code}_1.jpeg";
                image1 = fileName;
                Product.FirstImage = fileName;
                break;
            case "image2":
                fileName = $"{Product.ClientId}_{Product.Code}_2.jpeg";
                image2 = fileName;
                Product.SecondImage = fileName;
                break;
            case "image3":
                fileName = $"{Product.ClientId}_{Product.Code}_3.jpeg";
                image3 = fileName;
                Product.ThirdImage = fileName;
                break;
            case "image4":
                fileName = $"{Product.ClientId}_{Product.Code}_4.jpeg";
                image4 = fileName;
                Product.FourthImage = fileName;
                break;
        }

        return fileName;
    }

    private void SavePicture(string image, string property)
    {
        try
        {
            switch (property)
            {
                case "detailImage":
                    detailImageData = image;
                    break;
                case "mainImage":
                    mainImageData = image;
                    break;
                case "image1":
                    image1Data = image;
                    break;
                case "image2":
                    image2Data = image;
                    break;
                case "image3":
                    image3Data = image;
                    break;
                case "image4":
                    image4Data = image;
                    break;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task HandleDeleteButtonClick(string gridSku, string gridEan)
    {
        var grid = Grids.Find(x => x.Sku == gridSku && x.Ean == gridEan);

        if (OnDeleteButtonClickAsync.HasDelegate)
        {
            OnDeleteButtonClickAsync.InvokeAsync(grid);
        }
        StateHasChanged();
    }

    private async Task SaveAsync()
    {
        _isProcessing = true;
        await form.Validate();
        if (form.IsValid && OnValidSubmitAsync.HasDelegate)
        {
            await OnValidSubmitAsync.InvokeAsync();
        }
        _isProcessing = false;
    }
}
