using Lab200.Data;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace Lab200.Pages.Company.VmUser;
public partial class CreateVmUser
{
    #region Injections
    [Inject] IVmUserService _vmUserService { get; set; }
    [Inject] ISessionState _sessionService { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    public Entities.VmUser VmUser { get; set; } = new ();
    private bool _isProcessing = false;
    private int _progressPercent = 0;

    private async Task HandleSaveButtonClick()
    {
        StateHasChanged();
        VmUser.ClientId = _sessionService.User.ClientId??32;
        _isProcessing = true;

        _progressPercent = 50;
        StateHasChanged();

        var isRegister = await _vmUserService.CreateVmUserAsync(VmUser);
        if (isRegister != 0)
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