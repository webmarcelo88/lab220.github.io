using Lab200.Entities;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.CompanyAssistantsRegistration.SectorComponents;
public partial class SectorForm
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
    public Entities.Sector Sector { get; set; } = null!;
    [Parameter]
    public bool IsParentLoading { get; set; } = false;

    [Parameter]
    public List<CostCenter> CostCenters { get; set; } = new();
    #endregion
    private string costCenter { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (Sector != null)
            costCenter = Sector.CostCenterId.ToString();

        if (Sector == null)
            Sector = new();

        await base.OnInitializedAsync();
    }

    private void SetCostCenter()
    {
        Sector.CostCenterId = int.Parse(costCenter);
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