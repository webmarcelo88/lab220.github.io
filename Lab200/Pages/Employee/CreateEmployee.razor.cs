using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Employee;

public partial class CreateEmployee
{
    #region Injections
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] IEmployeeService EmployeeService { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] IItemService ItemService { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }
    #endregion


    public Entities.Employee NewEmployee { get; set; } = new();
    public List<Item> Items { get; set; } = new();

    private async Task HandleSectorIdChange()
    {
        StateHasChanged();

        if (NewEmployee.SectorId == null)
            Items = new();
        else
            Items = await ItemService.GetItemsByClientAndSector(NewEmployee.SectorId.Value, NewEmployee.ClientId);

        StateHasChanged();
    }

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();

        NewEmployee.ClientId = _sessionState.User.ClientId ?? 32;

        var successfullySaved = await EmployeeService.AddNewEmployee(NewEmployee);
        if(successfullySaved == 0)
        {
            Snackbar.Add($"Não foi possível cadastrar o funcionário: {NewEmployee.Name}!", Severity.Error);
            return;
        }
        else
        {
            Snackbar.Add($"Funcionário cadastrado com sucesso!",Severity.Success);
            NavigationManager.NavigateTo(Routes.ALL_EMPLOYEES);
        }

    }
}
