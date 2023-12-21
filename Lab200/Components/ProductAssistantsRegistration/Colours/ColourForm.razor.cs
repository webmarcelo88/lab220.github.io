using Lab200.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.ProductAssistantsRegistration.Colours;

public partial class ColourForm
{
    MudForm form;
    public bool _isLoading = false;
    public bool _isProcessing = false;
    public bool success;
    public string[] errors = { };

    [Inject] ISnackbar _snackBar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;

    #region Parameters
    [Parameter]
    public EventCallback OnValidSubmitAsync { get; set; }

    [Parameter]
    public Entities.Colours Colour { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion

    protected async override Task OnInitializedAsync()
    {
        if (Colour == null)
            Colour = new Entities.Colours();
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
