using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;

namespace Lab200.Pages.Employee;

public partial class Employees
{
    #region Injections
    [Inject] ISnackbar _snackBar { get; set; } = null!;
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    [Inject] ProtectedLocalStorage protectedLocalStorage { get; set; } = null!;
    [Inject] IEmployeeService _employeeService { get; set; } = null!;
    #endregion

    private List<Entities.Employee> EmployeesList { get; set; } = new();
    private string Search { get; set; }

    #region Design Parameters
    private bool IsTableLoading { get; set; } = true;
    #endregion

    protected override async Task OnInitializedAsync()
    {
        StateHasChanged();
        EmployeesList = await GetEmployeesAsync();
        IsTableLoading = !IsTableLoading;
        await base.OnInitializedAsync();
    }

    private async Task<List<Entities.Employee>> GetEmployeesAsync()
    {
        return await _employeeService.GetEmployeesAsync();
    } 

    private void AddNewEmployee() => _navigationManager.NavigateTo(Routes.CREATE_NEW_EMPLOYEE);

    private async Task DeleteEmployee(int employeeId)
    {
        var employee = EmployeesList.Where(x => x.Id == employeeId).FirstOrDefault();

        if(employee is null)
        {
            _snackBar.Add($"Funcionário não encontrado!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(employee.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _employeeService.DeleteEmployeeAsync(employee);
            if (removed > 0)
            {
                _snackBar.Add($"Funcionário {employee.Name} removido com sucesso!", MudBlazor.Severity.Success);
                EmployeesList = await GetEmployeesAsync();
                EmployeesList.Remove(employee);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.ALL_EMPLOYEES);
            }
            else
            {
                _snackBar.Add($"Erro ao remover funcionário {employee.Name}!", MudBlazor.Severity.Error);
            }
            #endregion
            StateHasChanged();
        }
        return ;
    }

    private async Task<bool> InvokeDeleteModalAsync(string employeeName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o funcionário: {employeeName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover Funcionário", parameters, dialogOptions);
        var result = await dialogResult.Result;

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }

    private async Task EditEmployee(int employeeId)
    {
        _navigationManager.NavigateTo($"/employees/{employeeId}/edit");
    }
}

