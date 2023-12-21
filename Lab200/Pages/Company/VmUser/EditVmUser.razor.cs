using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages.Company.VmUser;
public partial class EditVmUser
{
    #region Injections
    [Inject] IVmUserService _vmUserService { get; set; }
    [Inject] ISessionState _sessionService { get; set; }
    [Inject] ISnackbar _snackbar { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    [Parameter] public int VmUserId { get; set; }

    public Entities.VmUser? VmUser { get; set; }


    private bool _isProcessing = false;
    private bool _isLoading = true;
    private int _progressPercent = 0;

    protected async override Task OnInitializedAsync()
    {
        VmUser = await _vmUserService.GetVmUserByIdAsync(VmUserId);
        if (VmUser == null)
        {
            _snackbar.Add($"Usuário não foi encontrado!", Severity.Error);
            _navigationManager.NavigateTo(Routes.COMPANY_VM_USER);
            return;
        }

        _isLoading = false;
    }

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var isUpdated = await _vmUserService.UpdateVmUserAsync(VmUser);
        if (isUpdated != 0)
        {
            _progressPercent = 75;
            StateHasChanged();
        }

        _progressPercent = 100;
        StateHasChanged();

        _navigationManager.NavigateTo(Routes.COMPANY_VM_USER);

        _isProcessing = false;
    }

}