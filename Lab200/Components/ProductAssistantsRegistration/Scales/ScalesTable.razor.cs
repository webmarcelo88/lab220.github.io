using Microsoft.AspNetCore.Components;

namespace Lab200.Components.ProductAssistantsRegistration.Scales;

public partial class ScalesTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }

    [Parameter]
    public List<Entities.Scale>? Scales { get; set; } = new(){
        new Entities.Scale(){
            Id = 1,
            Name = "P",
            Order = 0
        },
                new Entities.Scale(){
            Id = 2,
            Name = "U",
            Order = 1
        },        new Entities.Scale(){
            Id = 3,
            Name = "M",
            Order = 1
        },        new Entities.Scale(){
            Id = 4,
            Name = "G",
            Order = 1
        },        new Entities.Scale(){
            Id = 5,
            Name = "GG",
            Order = 2
        },        new Entities.Scale(){
            Id = 6,
            Name = "XP",
            Order = 0
        },
    };

    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;
    #endregion

    #region Design Parameters
    private string[] Headings { get; set; } = { "ID", "Descrição", "Ordem" };
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
