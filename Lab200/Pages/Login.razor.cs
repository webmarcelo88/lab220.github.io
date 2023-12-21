using Lab200.Entities;
using Lab200.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Lab200.Pages;

public partial class Login
{
    [Inject] ISessionState _sessionState { get; set; } = null!;
    [Inject] ISnackbar _snackbar { get; set; } = null!;
    [Inject] NavigationManager _navigationManager { get; set; } = null!;


    public bool _isLoading = false;
    public bool _isProcessing = false;
    public bool success;
    public string[] errors = { };
    MudForm form;

    private string email { get; set; } = null!;
    private string password { get; set; } = null!;

    private async Task HandleLoginButtonClick()
    {
        _isLoading = true;
        if(string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(password))
        {
            _snackbar.Add($"Email ou senha estão errados", Severity.Error);
            _isLoading = false;
            StateHasChanged();
            return;
        }

        var isLoggedIn = await _sessionState.LoginAsync(email, password);
        if(!isLoggedIn)
        {
            _snackbar.Add($"Email ou senha estão errados", Severity.Error);
            _isLoading = false;
            StateHasChanged();
            return;
        }

        _isLoading = false;
        _navigationManager.NavigateTo("/home");
    }

}
