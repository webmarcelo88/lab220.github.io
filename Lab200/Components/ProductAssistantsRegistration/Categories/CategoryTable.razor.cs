using Lab200.Entities;
using Microsoft.AspNetCore.Components;

namespace Lab200.Components.ProductAssistantsRegistration.Categories;
public partial class CategoryTable
{
    #region Parameters
    [Parameter]
    public EventCallback<int> OnDeleteButtonClick { get; set; }

    [Parameter]
    public EventCallback<int> OnEditButtonClick { get; set; }
    [Parameter]
    public List<Category>? Categories { get; set; } = new()
    {
        new(){
            Id = 1,
            ClientId = 101,
            Name = "Electronics",
            Image = "electronics.jpg",
            Order = 1,
            IsDeleted = false
        }
    };
    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;
    #endregion

    string[] categoriesTableHeadings = { "ID", "Image", "Nome", "Ordem" };

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