using Lab200.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.CompanyAssistantsRegistration.FunctionComponents;
public partial class FunctionForm
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
    public Entities.Function Function { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;

    [Parameter]
    public List<CostCenter> CostCenters { get; set; } = new();
    #endregion

    private string costCenter { get; set; }

    protected async override Task OnInitializedAsync()
    {
        Function ??= new Entities.Function();

        if (Function.CostCenter != null)
            costCenter = Function.CostCenter.Id.ToString();

        await base.OnInitializedAsync();
    }

    private void SetCostCenter()
    {
        Function.CostCenterId = int.Parse(costCenter);
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