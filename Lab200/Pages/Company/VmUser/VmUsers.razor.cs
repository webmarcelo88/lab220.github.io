using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Company.VmUser;
public partial class VmUsers
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; }
    [Inject] ISessionState _sessionService { get; set; }
    [Inject] IVmUserService _vmUserService { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    private List<Entities.VmUser> VmUsersList { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        VmUsersList = await _vmUserService.GetAllVmUsersAsync(_sessionService.User.ClientId ?? 32);
        // UsersList = await _userService.GetAllUsersAsync(32);
        IsTableLoading = false;
    }
    private void AddNewUser() => _navigationManager.NavigateTo(Routes.CREATE_COMPANY_VM_USER);
    private async Task DeleteUser(int vmUserId)
    {
        var vmUser = VmUsersList.Where(x => x.Id == vmUserId).FirstOrDefault();

        if (vmUser is null)
        {
            _snackbar.Add($"Usuário não encontrado!", Severity.Error);
            StateHasChanged();
            return;
        }

        var shouldCancel = await InvokeDeleteModalAsync(vmUser!.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _vmUserService.DeleteVmUserAsync(vmUser);
            if (removed > 0)
            {
                _snackbar.Add($"Usuário {vmUser.Name} removido com sucesso!", Severity.Success);
                VmUsersList = await _vmUserService.GetAllVmUsersAsync(_sessionService.User.ClientId ?? 32);
                VmUsersList.Remove(vmUser);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.COMPANY_VM_USER);
            }
            else
            {
                _snackbar.Add($"Erro ao remover usuário {vmUser.Name}!", Severity.Error);
            }
            #endregion
            StateHasChanged();
        }
        return;
    }
    private void EditUser(int userId)
    {
        _navigationManager.NavigateTo($"/company/vm-users/{userId}/edit");
    }

    private async Task<bool> InvokeDeleteModalAsync(string vmUserName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o usuário: {vmUserName}?" },
            { "ButtonText", "Sim" }
        };

        var dialogOptions = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, ClassBackground = "blur", FullWidth = true };

        var dialogResult = _dialogService.Show<DeleteConfirmationDialog>("Remover usuário", parameters, dialogOptions);
        var result = await dialogResult.Result;
        dialogResult.Close();
        dialogResult.Dismiss(result);

        return !result.Canceled && bool.TryParse(result.Data.ToString(), out bool resultbool);
    }
}