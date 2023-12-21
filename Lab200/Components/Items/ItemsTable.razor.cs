using Lab200.Entities;
using Microsoft.AspNetCore.Components;

namespace Lab200.Components.Items;

public partial class ItemsTable
{
    #region Parameters
    [Parameter]
    public List<Item> Items { get; set; } = new();

    [Parameter]
    public bool IsTableLoading { get; set; } = false;

    [Parameter]
    public bool HasFixedFooter { get; set; } = false;

    [Parameter]
    public bool HasFixedHeader { get; set; } = true;
    #endregion

    string[] itemsTableHeadings = { "ID", "Sku", "Produto", "Tamanho", "Cor", "Categoria", "Ordem" };
}

