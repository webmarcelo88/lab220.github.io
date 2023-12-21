using Lab200.Entities;
using Microsoft.AspNetCore.Components;

namespace Lab200.Components.Configurations.Themes;

public partial class ThemesTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }
    [Parameter]
    public List<Entities.Theme>? Themes { get; set; } = new();
    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;
    #endregion

    string[] themesTableHeadings = { "ID", "Image", "Nome", "Layout" };

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