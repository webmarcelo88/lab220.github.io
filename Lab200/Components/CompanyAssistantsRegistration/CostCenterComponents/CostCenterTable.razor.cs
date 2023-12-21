using Lab200.Entities;
using Microsoft.AspNetCore.Components;

namespace Lab200.Components.CompanyAssistantsRegistration.CostCenterComponents;
public partial class CostCenterTable
{
    #region Parameters

    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }

    [Parameter]
    public List<CostCenter>? CostCenters { get; set; } = new();

    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;
    #endregion

    string[] costCenterTableHeadings = { "ID", "Código", "Descrição", "Ordem" };

    private async Task HandleDeleteButtonClick(int costCenterId)
    {
        if (OnDeleteButtonClick.HasDelegate)
        {
            await OnDeleteButtonClick.InvokeAsync(costCenterId);
            StateHasChanged();
        }
    }

    private async Task HandleEditButtonClick(int costCenterId)
    {
        if (OnEditButtonClick.HasDelegate)
        {
            await OnEditButtonClick.InvokeAsync(costCenterId);
            StateHasChanged();
        }
    }
}