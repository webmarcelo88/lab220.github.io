using Lab200.Data;
using Lab200.Entities;
using Lab200.Interfaces;
using Lab200.Interfaces.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text.RegularExpressions;

namespace Lab200.Components.Company.WebUser;

public partial class WebUserForm
{
    #region Injections
    [Inject] IPlantService _plantService { get; set; }
    [Inject] ISessionState _sessionState { get; set; }
    [Inject] NavigationManager _navigationManager { get; set; }
    #endregion

    #region Parameters
    [Parameter]
    public EventCallback OnValidSubmitAsync { get; set; }

    [Parameter]
    public User? User { get; set; }
    [Parameter]
    public bool IsParentLoading { get; set; } = false;
    #endregion

    public bool _isLoading = true;
    public bool _isProcessing = false;
    private string _confirmPass = string.Empty;
    private string _accountType = string.Empty;
    private string _plantId = string.Empty;
    private string[] AccountTypes = Roles.AllRoles;
    private Plant[] Plants;

    public bool success;
    public string[] errors = { };
    MudTextField<string> pwField1;
    MudForm form;

    protected async override Task OnInitializedAsync()
    {
        var plants = await _plantService.GetAllPlantsByClientAsync(_sessionState.User.ClientId);
        Plants = plants.ToArray();

        if(User != null)
        {
            _confirmPass = User.Password;
        }

        if (User == null)
        {
            User = new();
            User.IsActive = true;
        }
        else
        {
            _accountType = User.Role;
            _plantId = User.PlantId?.ToString();
        }
        _isLoading = false;
    }
    private async Task SaveAsync()
    {
        _isProcessing = true;
        await form.Validate();
        if (form.IsValid && OnValidSubmitAsync.HasDelegate)
        {
            await OnValidSubmitAsync.InvokeAsync();
        }
        _isProcessing = false;
    }

    private void SetUserRole(IEnumerable<string> role)
    {
        var selectedRole = role.FirstOrDefault();

        if (User == null)
            User = new();

        User.Role = AccountTypes.Where(x => x.Equals(selectedRole, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

        if (selectedRole != null)
            _accountType = selectedRole;
        else
            _accountType = string.Empty;

    }

    private void SetPlant(IEnumerable<string> plant)
    {
        var selectedPlant = int.Parse(plant.FirstOrDefault());

        if (User == null)
            User = new();

        User.PlantId = selectedPlant;

        if (selectedPlant != null)
            _plantId = selectedPlant.ToString();
        else
            _plantId = string.Empty;

    }

    private IEnumerable<string> PasswordStrength(string pw)
    {
        if (string.IsNullOrWhiteSpace(pw))
        {
            yield return "Senha é obrigatória!!";
            yield break;
        }
        if (!Regex.IsMatch(pw, @"[0-9]"))
            yield return "Senha deve conter ao menos um número";
    }

    private string PasswordMatch(string arg)
    {
        if (pwField1.Value != arg)
            return "As senhas não batem!";
        return null;
    }
}
