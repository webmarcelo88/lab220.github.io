using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.CompanyAssistantsRegistration.CostCenterComponents;
public partial class CostCenterForm
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
    public Entities.CostCenter CostCenter { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion

    protected async override Task OnInitializedAsync()
    {
        CostCenter ??= new Entities.CostCenter();

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