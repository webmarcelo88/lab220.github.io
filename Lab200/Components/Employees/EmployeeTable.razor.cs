using System;
using Microsoft.AspNetCore.Components;

namespace Lab200.Components.Employees;

public partial class EmployeeTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }

    [Parameter]
    public List<Entities.Employee>? EmployeesList { get; set; }

    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;

    #endregion

    #region Design Parameters
    private string[] Headings { get; set; } = { "ID", "Nome", "Matrícula", "Ordem" };
    #endregion

    private async Task HandleDeleteButtonClick(int employeeId)
    {
        if (OnDeleteButtonClick.HasDelegate)
        {
            await OnDeleteButtonClick.InvokeAsync(employeeId);
            StateHasChanged();
        }
    }

    private async Task HandleEditButtonClick(int employeeId)
    {
        if (OnEditButtonClick.HasDelegate)
        {
            await OnEditButtonClick.InvokeAsync(employeeId);
            StateHasChanged();
        }
    }
}

