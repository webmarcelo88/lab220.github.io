using Microsoft.AspNetCore.Components;

namespace Lab200.Components.Company.WebUser;

public partial class WebUserTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }

    [Parameter]
    public List<Entities.User>? Users { get; set; }

    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;

    #endregion

    #region Design Parameters
    private string[] Headings { get; set; } = { "ID", "Nome", "E-mail", "Perfil", "Ativo","Último Login" };
    #endregion

    private async Task HandleDeleteButtonClick(int userId)
    {
        if (OnDeleteButtonClick.HasDelegate)
        {
            await OnDeleteButtonClick.InvokeAsync(userId);
            StateHasChanged();
        }
    }

    private async Task HandleEditButtonClick(int userId)
    {
        if (OnEditButtonClick.HasDelegate)
        {
            await OnEditButtonClick.InvokeAsync(userId);
            StateHasChanged();
        }
    }
}
