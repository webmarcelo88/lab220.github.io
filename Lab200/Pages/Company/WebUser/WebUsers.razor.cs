using Lab200.Components.Shared;
using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Company.WebUser;

public partial class WebUsers
{
    #region Injection
    [Inject] IDialogService _dialogService { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] IUserService _userService { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    private List<Entities.User> UsersList { get; set; } = new();
    private bool IsTableLoading { get; set; } = true;

    protected async override Task OnInitializedAsync()
    {
        StateHasChanged();
        UsersList = await _userService.GetAllUsersAsync(_sessionState.User.ClientId ?? 32);
        IsTableLoading = false;
    }

    private void AddNewUser() => _navigationManager.NavigateTo(Routes.CREATE_COMPANY_WEB_USER);

    private async Task DeleteUser(int userId)
    {
        var user = UsersList.Where(x => x.Id == userId).FirstOrDefault();

        if (user is null)
        {
            _snackbar.Add($"Usuário não encontrado!", Severity.Error);
            StateHasChanged();
        }

        var shouldCancel = await InvokeDeleteModalAsync(user.Name);
        if (shouldCancel)
        {
            #region Delete With service implementation
            var removed = await _userService.DeleteUserAsync(user);
            if (removed > 0)
            {
                _snackbar.Add($"Usuário {user.Name} removido com sucesso!", MudBlazor.Severity.Success);
                UsersList = await _userService.GetAllUsersAsync(_sessionState.User.ClientId ?? 32);
                UsersList.Remove(user);
                StateHasChanged();
                _navigationManager.NavigateTo(Routes.COMPANY_WEB_USER);
            }
            else
            {
                _snackbar.Add($"Erro ao remover usuário {user.Name}!", MudBlazor.Severity.Error);
            }
            #endregion
            StateHasChanged();
        }
        return;
    }
    private void EditUser(int userId)
    {
        _navigationManager.NavigateTo($"/company/web-users/{userId}/edit");
    }

    private async Task<bool> InvokeDeleteModalAsync(string employeeName)
    {
        var parameters = new DialogParameters
        {
            { "ContentText", $"Deseja remover o usuário: {employeeName}?" },
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
