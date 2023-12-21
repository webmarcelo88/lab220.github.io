using Lab200.Entities;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.EmployeeItem;

public partial class CreateEmployeeItem
{
    #region Injections
    [Inject] IGridService _gridService { get; set; }
    [Inject] IEmployeeItemsService _employeeItemsService { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    #endregion

    #region Parameters
    [Parameter]
    public Action<string>? CloseModal { get; set; }

    [Parameter]
    public int EmployeeId { get; set; } = 81;
    [Parameter]
    public int ClientId { get; set; } = 32;
    #endregion

    public List<Grid>? ClientGrids { get; set; }
    public bool Loading { get; set; } = true;
    public bool Processing { get; set; } = false;
    public EmployeeItems NewEmployeeItem { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        ClientGrids = await _gridService.GetGridsByClientAsync(ClientId);
        Loading = false;
        NewEmployeeItem.ClientId = ClientId;
        NewEmployeeItem.EmployeeId = EmployeeId;
    }

    private void SetNewEmployeeItemInfo(Grid? item)
    {
        if (item is null)
        {
            NewEmployeeItem.ProductId = NewEmployeeItem.ProductId = NewEmployeeItem.ColorsId = 0;
            return;
        }

        NewEmployeeItem.ProductId = item.ProductId;
        NewEmployeeItem.ScaleId = item.ScaleId;
        NewEmployeeItem.ColorsId = item.ColorsId;
        NewEmployeeItem.Sku = item.Product.Code;

        StateHasChanged();
    }

    private async Task CreateNewEmployeeItemAsync()
    {
        if(CloseModal != null)
        {
            _snackbar.Add($"FECHANDOO!", Severity.Success);
            CloseModal?.Invoke($"Reza a lenda que isso funciona");
            StateHasChanged();
        }

        //if (!ValidateNewEmployeeItem())
        //{
        //    _snackbar.Add($"Preencha os campos com valores válidos!", Severity.Error);
        //    return;
        //}
        //    int? item = null;

        //Processing = true;

        //item = await _employeeItemsService.AddNewEmployeeItem(NewEmployeeItem);

        //await Task.Delay(5000);
        //Processing = false;
    }

    private bool ValidateNewEmployeeItem()
    {
        return NewEmployeeItem.ProductId != (int)default &&
            NewEmployeeItem.ScaleId != (int)default &&
            NewEmployeeItem.ColorsId != (int)default;
    }
}