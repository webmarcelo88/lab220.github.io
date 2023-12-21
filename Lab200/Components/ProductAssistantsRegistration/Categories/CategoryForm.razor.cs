using Lab200.Entities;
using Lab200.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.IO;

namespace Lab200.Components.ProductAssistantsRegistration.Categories;
public partial class CategoryForm
{
    [Inject] ISessionState _sessionState { get; set; }

    MudForm form;
    public bool _isLoading = false;
    public bool _isProcessing = false;
    public bool success;
    public string[] errors = { };

    IBrowserFile picture = null!;
    string imageData = null!;

    #region Injections
    [Inject] ISnackbar _snackBar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    [Inject] IWebHostEnvironment _webHostEnvironment { get; set; } = null!;
    #endregion

    #region Parameters
    [Parameter]
    public EventCallback OnValidSubmitAsync { get; set; }

    [Parameter]
    public Category Category { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion

    protected async override Task OnInitializedAsync()
    {
        if (Category == null)
            Category = new Category();
        else if (Category.Id != (int)default)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string imagesPath = Path.Combine(wwwrootPath, "images\\categorias");

            imageData = $"{imagesPath}\\{Category.Image}";
        }
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

    private async Task OnPictureSelectionAsync(InputFileChangeEventArgs e)
    {
        var format = "image/jpeg";

        foreach (var image in e.GetMultipleFiles(int.MaxValue))
        {
            var resizedImage = await image.RequestImageFileAsync(format, 800, 480);
            var buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            imageData = $"data:{format};base64, {Convert.ToBase64String(buffer)}";
            SavePicture(imageData, _sessionState.User.ClientId ?? 32, Category.Name);
        }
    }

    private void SavePicture(string image, int clientId, string categoryName)
    {
        try
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string imagesPath = Path.Combine(wwwrootPath, "images\\categorias");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
            if (File.Exists($"{imagesPath}\\{clientId}_{categoryName}.jpeg"))
            {
                _snackBar.Add("Imagem atualizada", Severity.Info);
            }

            string fileName = $"{clientId}_{categoryName}.jpeg";

            string fullPath = Path.Combine(imagesPath, fileName);

            byte[] imagemBytes = Convert.FromBase64String(image.Split(',')[1]);

            File.WriteAllBytes(fullPath, imagemBytes);

            Category.Image = fileName;
        }
        catch (Exception)
        {

            throw;
        }
    }
}