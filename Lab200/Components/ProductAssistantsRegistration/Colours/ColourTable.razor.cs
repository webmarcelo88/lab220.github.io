using Microsoft.AspNetCore.Components;

namespace Lab200.Components.ProductAssistantsRegistration.Colours;
public partial class ColourTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }

    [Parameter]
    public List<Entities.Colours>? Colours { get; set; } = new(){
        new Entities.Colours(){
            Id = 1,
            Name = "Preto",
            Value = "#000000"
        },
                new Entities.Colours(){
            Id = 2,
            Name = "Branco",
            Value = "#ffffff"
        },        new Entities.Colours(){
            Id = 3,
            Name = "Vermelho",
            Value = "#d70000"
        },        new Entities.Colours(){
            Id = 4,
            Name = "Verde",
            Value = "#00f102"
        },        new Entities.Colours(){
            Id = 5,
            Name = "Amarelo",
            Value = "#fcfa00"
        },        new Entities.Colours(){
            Id = 6,
            Name = "Roxo",
            Value = "#9400f9"
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
    private string[] Headings { get; set; } = { "ID", "Visual", "Nome" };
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