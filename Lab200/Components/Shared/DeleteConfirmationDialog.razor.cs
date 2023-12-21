using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Components.Shared;

public partial class DeleteConfirmationDialog : ComponentBase
{
    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public string ContentText { get; set; }
    [Parameter]
    public string ButtonText { get; set; }

    void Submit() => MudDialog.Close(DialogResult.Ok(true));
    void Cancel() => MudDialog.Cancel();
}

