using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.CompanyAssistantsRegistration.CostCenters;

public partial class CostCentersList
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; }
    [Inject] ICostCenterService _costCenterService { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    private List<Entities.CostCenter> AllCostCenters { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        AllCostCenters = await _costCenterService.GetCostCentersByClientAsync(_sessionState.User.ClientId ?? 32);
        // UsersList = await _userService.GetAllUsersAsync(32);
        IsTableLoading = false;
    }

    private void AddNewCostCenter() => _navigationManager.NavigateTo(Routes.CREATE_COST_CENTERS);

    private async Task DeleteCostCenter(int costCenterId)
    {
        var costCenter = AllCostCenters.Where(x => x.Id == costCenterId).FirstOrDefault();

        if (costCenter is null)
        {
            _snackbar.Add($"Centro de custo não encontrado!", Severity.Error);
            AllCostCenters = await _costCenterService.GetCostCentersByClientAsync(_sessionState.User.ClientId ?? 32);
            StateHasChanged();
            return;
        }

        var shouldDelete = await InvokeDeleteModalAsync(costCenter.Name);
        if (shouldDelete)
        {
            var removed = await _costCenterService.DeleteCostCenterAsync(costCenter);
            AllCostCenters = await _costCenterService.GetCostCentersByClientAsync(_sessionState.User.ClientId ?? 32);
            AllCostCenters.Remove(costCenter);
            StateHasChanged();
            _navigationManager.NavigateTo(Routes.COST_CENTERS);
        }
        else
        {
            _snackbar.Add($"Erro ao remover o centro de custo {costCenter.Name}!", Severity.Error);
        }

        StateHasChanged();
    }

    private void EditCostCenter(int costCenterId)
    {
        _navigationManager.NavigateTo($"/company-assistants/cost-center/{costCenterId}/edit");
    }

    private async Task<bool> InvokeDeleteModalAsync(string costCenterName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o centro de custo: {costCenterName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover centro de custo", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}
