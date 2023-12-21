using Lab200.Entities;
using Microsoft.AspNetCore.Components;

namespace Lab200.Components.Company.VmUser;

public partial class VmUserTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }

    [Parameter]
    public List<Entities.VmUser>? VmUsers { get; set; } 

    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;

    #endregion

    #region Design Parameters
    private string[] Headings { get; set; } = { "ID", "Nome", "Login", "Ativo" };
    #endregion


    private async Task HandleDeleteButtonClick(int vmUserId)
    {
        if (OnDeleteButtonClick.HasDelegate)
        {
            await OnDeleteButtonClick.InvokeAsync(vmUserId);
            StateHasChanged();
        }
    }

    private async Task HandleEditButtonClick(int vmUserId)
    {
        if (OnEditButtonClick.HasDelegate)
        {
            await OnEditButtonClick.InvokeAsync(vmUserId);
            StateHasChanged();
        }
    }
}
