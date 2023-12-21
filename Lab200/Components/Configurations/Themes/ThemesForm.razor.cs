using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Lab200.Entities;

namespace Lab200.Components.Configurations.Themes;

public partial class ThemesForm
{
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
    public Entities.Theme Theme { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion

    protected async override Task OnInitializedAsync()
    {
        if (Theme == null)
            Theme = new Theme();
        else if (Theme.Id != (int)default)
        {
            string imagesPath = Path.Combine("images\\temas");

            imageData = $"{imagesPath}\\{Theme.Image}";
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
        Random random = new Random();

        int maxDigits = 1000000; // 10^6
        int randomNumber = random.Next(maxDigits);


        foreach (var image in e.GetMultipleFiles(int.MaxValue))
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string imagesPath = Path.Combine(wwwrootPath, $"images\\temas\\{Theme.ClientId}_{randomNumber}.jpeg");
            await using FileStream fs = new(imagesPath, FileMode.Create);
            await image.OpenReadStream().CopyToAsync(fs);
            imageData = $"images\\temas\\{Theme.ClientId}_{randomNumber}.jpeg";
            Theme.Image = $"{Theme.ClientId}_{ randomNumber}.jpeg";
            StateHasChanged();
        }
    }
}