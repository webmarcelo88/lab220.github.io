using System;
using MudBlazor;

namespace Lab200.Pages.Testing;

public partial class ModalTesting
{
    public string? Content { get; set; } = string.Empty;
    public bool IsVisible { get; set; } = true;
    public bool IsProcessing { get; set; } = true;
    public DialogOptions DialogOptions { get; set; } = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.Medium, ClassBackground = "blur", FullWidth = true };

    public void Close(string data)
    {
        IsProcessing = false;
        StateHasChanged();
    }
}

