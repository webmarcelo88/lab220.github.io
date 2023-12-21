using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Employee;

public partial class EditEmployee
{
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] ICostCenterService _costCenterService { get; set; }
    [Inject] IPlantService _plantService { get; set; }
    [Inject] IEmployeeService EmployeeService { get; set; }
    [Inject] IItemService ItemService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    [Parameter] public int EmployeeId { get; set; }

    public Entities.Employee Employee { get; set; } = new();
    public List<Item> Items { get; set; } = new();
    public List<CostCenter>? CostCenters { get; set; }
    public List<Plant>? Plants { get; set; }

    public bool IsLoading { get; set; } = true;


    protected async override Task OnInitializedAsync()
    {
        Employee = await EmployeeService.GetByIdAsync(EmployeeId) ?? new();

        if (Employee.SectorId == null)
            Items = new();
        else
            Items = await ItemService.GetItemsByClientAndSector(Employee.SectorId.Value, Employee.ClientId);

        if(Employee.ClientId != 0)
        {
            Plants = await _plantService.GetAllPlantsByClientAsync(Employee.ClientId);
            CostCenters = await _costCenterService.GetCostCentersByClientAsync(Employee.ClientId);
        }

        IsLoading = false;
    }

    private async Task HandleSectorIdChange()
    {
        if (Employee.SectorId == null)
            Items = new();
        else
            Items = await ItemService.GetItemsByClientAndSector(Employee.SectorId.Value, Employee.ClientId);
    }

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();

        var successfullySaved = await EmployeeService.UpdateEmployeeAsync(Employee);
        if (successfullySaved == 0)
        {
            Snackbar.Add($"Não foi possível atualizar os dados do funcionário: {Employee.Name}!", Severity.Error);
            return;
        }
        else
        {
            Snackbar.Add($"Dados atualizados com sucesso!", Severity.Success);
            NavigationManager.NavigateTo(Routes.ALL_EMPLOYEES);
        }

    }
}
