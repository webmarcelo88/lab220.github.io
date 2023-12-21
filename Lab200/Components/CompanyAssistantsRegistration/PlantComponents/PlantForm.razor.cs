using Lab200.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.CompanyAssistantsRegistration.PlantComponents;

public partial class PlantForm
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
    public Plant Plant { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion


    protected async override Task OnInitializedAsync()
    {
        Plant ??= new Plant();

        await base.OnInitializedAsync();
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
