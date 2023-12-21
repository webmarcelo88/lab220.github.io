using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Lab200.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.Function;

public partial class FunctionsList
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; } = null!;
    [Inject] ISessionState _sessionState { get; set; } = null;
    [Inject] IFunctionService _functionService { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;
    #endregion

    private List<Entities.Function> Functions { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {

        StateHasChanged();
        Functions = await _functionService.GetFunctionsByClientAndCostCenterAsync(_sessionState.User.ClientId ?? 32, 29);
        IsTableLoading = false;
    }

    private void AddNewFunction() => _navigationManager.NavigateTo(Routes.CREATE_FUNCTIONS);

    private async Task DeleteFunction(int functionId)
    {
        var function = Functions.Where(x=>x.Id == functionId).FirstOrDefault();

        if(function == null)
        {
            _snackbar.Add($"Função não encontrada!", Severity.Error);
            Functions = await _functionService.GetFunctionsByClientAndCostCenterAsync(_sessionState.User.ClientId ?? 32, 29);
            StateHasChanged();
            return;
        }

        var shouldDelete = await InvokeDeleteModalAsync(function.Name);
        if (shouldDelete)
        {
            var removed = await _functionService.DeleteFunctionAsync(function);
            Functions = await _functionService.GetFunctionsByClientAndCostCenterAsync(_sessionState.User.ClientId ?? 32, 29);
            Functions.Remove(function);
            StateHasChanged();
            _navigationManager.NavigateTo(Routes.FUNCTIONS);
        }
        else
        {
            _snackbar.Add($"Erro ao remover a função {function.Name}!", Severity.Error);
        }

        StateHasChanged();
    }
    private void EditFunction(int functionId)
    {
        _navigationManager.NavigateTo($"/company-assistants/functions/{functionId}/edit");
    }

    private async Task<bool> InvokeDeleteModalAsync(string functionName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover a função: {functionName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover função", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}